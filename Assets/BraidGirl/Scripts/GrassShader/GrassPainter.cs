using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;



[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class GrassPainter : MonoBehaviour
{

    public Mesh mesh;
    private MeshFilter _filter;
    public int grassLimit = 50000;

    // options
    public int toolbarInt = 0;
    public int toolbarIntEdit = 0;

    // mesh lists
    [SerializeField]
    private List<Vector3> _positions = new List<Vector3>();
    [SerializeField]
    private List<Color> _colors = new List<Color>();
    [SerializeField]
    private List<int> _indicies = new List<int>();
    [SerializeField]
    private List<Vector3> _normals = new List<Vector3>();
    [SerializeField]
    private List<Vector2> _length = new List<Vector2>();
    private int[] _indi;
    public int i = 0;

    // brush settings
    public LayerMask hitMask = 1;
    public LayerMask paintMask = 1;
    public float brushSize;
    public float brushFalloffSize;
    public float Flow;
    public float density = 1f;
    public float normalLimit = 1;

    // length/width
    public float sizeWidth = 1f;
    public float sizeLength = 1f;

    // reproject settings
    public float reprojectOffset = 1f;

    // color settings
    public float rangeR, rangeG, rangeB;
    public Color AdjustedColor;

    // raycast vars
    [HideInInspector]
    public Vector3 hitPosGizmo;
    private Vector3 _mousePos;
    private Vector3 _hitPos;
    [HideInInspector]
    public Vector3 hitNormal;

    private int _flowTimer;
    private Vector3 _lastPosition = Vector3.zero;

#if UNITY_EDITOR
    public void OnFocus()
    {
        // Remove delegate listener if it has previously
        // been assigned.
        if (gameObject == null)
        {
            return;
        }
        SceneView.duringSceneGui -= this.OnScene;
        // Add (or re-add) the delegate.
        SceneView.duringSceneGui += this.OnScene;
    }

    public void HandleUndo()
    {
        if (mesh)
        {
            mesh.GetVertices(_positions);
            if (_positions.Count == 0)
            {
                ClearMesh();
                return;
            }
            i = _positions.Count;
            mesh.GetIndices(_indicies, 0);
            _indi = _indicies.ToArray();
            mesh.GetUVs(0, _length);
            mesh.GetColors(_colors);
            mesh.GetNormals(_normals);
            RebuildMesh();
        }
        SceneView.RepaintAll();
    }

    public void OnDestroy()
    {
        // When the window is destroyed, remove the delegate
        // so that it will no longer do any drawing.
        SceneView.duringSceneGui -= this.OnScene;
        Undo.undoRedoPerformed -= this.HandleUndo;
    }

    private void OnEnable()
    {
        if (_filter == null)
        {
            _filter = GetComponent<MeshFilter>();
        }
        SceneView.duringSceneGui += this.OnScene;
        Undo.undoRedoPerformed += this.HandleUndo;

        SetupMesh();
    }

    public void SetupMesh()
    {
        mesh.GetVertices(_positions);
        i = _positions.Count;
        mesh.GetIndices(_indicies, 0);
        _indi = _indicies.ToArray();
        mesh.GetUVs(0, _length);
        mesh.GetColors(_colors);
        mesh.GetNormals(_normals);
    }

    public void ClearMesh()
    {
        Undo.RegisterCompleteObjectUndo(mesh, "Cleared Grass");
        i = 0;
        _positions = new List<Vector3>();
        _indicies = new List<int>();
        _colors = new List<Color>();
        _normals = new List<Vector3>();
        _length = new List<Vector2>();
        RebuildMesh();
    }

    public void FloodColor()
    {
        Undo.RegisterCompleteObjectUndo(mesh, "Flooded Color");
        for (int i = 0; i < _colors.Count; i++)
        {
            _colors[i] = AdjustedColor;
        }
        RebuildMesh();
    }

    public void FloodLengthAndWidth()
    {
        Undo.RegisterCompleteObjectUndo(mesh, "Flooded Length/Width");
        for (int i = 0; i < _length.Count; i++)
        {
            _length[i] = new Vector2(sizeWidth, sizeLength);
        }
        RebuildMesh();
    }

    public void OnScene(SceneView scene)
    {
        if (this != null)
        {
            // only allow painting while this object is selected
            if ((Selection.Contains(gameObject)))
            {
                Event e = Event.current;
                RaycastHit terrainHit;
                _mousePos = e.mousePosition;
                float ppp = EditorGUIUtility.pixelsPerPoint;
                _mousePos.y = scene.camera.pixelHeight - _mousePos.y * ppp;
                _mousePos.x *= ppp;
                _mousePos.z = 0;

                // ray for gizmo(disc)
                Ray rayGizmo = scene.camera.ScreenPointToRay(_mousePos);
                RaycastHit hitGizmo;

                if (Physics.Raycast(rayGizmo, out hitGizmo, 200f, hitMask.value))
                {
                    hitPosGizmo = hitGizmo.point;
                }
                // undo system
                if (e.type == EventType.MouseDown && e.button == 1)
                {
                    if (toolbarInt == 0)
                    {
                        Undo.RegisterCompleteObjectUndo(mesh, "Added Grass");
                    }
                    else if (toolbarInt == 1)
                    {
                        Undo.RegisterCompleteObjectUndo(mesh, "Removed Grass");
                    }
                    else if (toolbarInt == 2)
                    {
                        Undo.RegisterCompleteObjectUndo(mesh, "Edited Grass");
                    }
                    else if (toolbarInt == 3)
                    {
                        Undo.RegisterCompleteObjectUndo(mesh, "Reprojected Grass");
                    }
                }
                if (e.type == EventType.MouseDrag && e.button == 1)
                {
                    if (toolbarInt == 0)
                    {
                        // place based on density
                        for (int k = 0; k < density * brushSize; k++)
                        {

                            // brushrange
                            float t = 2f * Mathf.PI * Random.Range(0f, brushSize);
                            float u = Random.Range(0f, brushSize) + Random.Range(0f, brushSize);
                            float r = (u > 1 ? 2 - u : u);
                            Vector3 origin = Vector3.zero;

                            // place random in radius, except for first one
                            if (k != 0)
                            {
                                origin.x += r * Mathf.Cos(t);
                                origin.y += r * Mathf.Sin(t);
                            }
                            else
                            {
                                origin = Vector3.zero;
                            }

                            // add random range to ray
                            Ray ray = scene.camera.ScreenPointToRay(_mousePos);
                            ray.origin += origin;

                            // if the ray hits something thats on the layer mask,  within the grass limit and within the y normal limit
                            if (Physics.Raycast(ray, out terrainHit, 200f, hitMask.value) && i < grassLimit && terrainHit.normal.y <= (1 + normalLimit) && terrainHit.normal.y >= (1 - normalLimit))
                            {
                                if ((paintMask.value & (1 << terrainHit.transform.gameObject.layer)) > 0)
                                {
                                    _hitPos = terrainHit.point;
                                    hitNormal = terrainHit.normal;
                                    if (k != 0)
                                    {
                                        var grassPosition = _hitPos;// + Vector3.Cross(origin, hitNormal);
                                        grassPosition -= this.transform.position;

                                        _positions.Add((grassPosition));
                                        _indicies.Add(i);
                                        _length.Add(new Vector2(sizeWidth, sizeLength));
                                        // add random color variations                          
                                        _colors.Add(new Color(AdjustedColor.r + (Random.Range(0, 1.0f) * rangeR), AdjustedColor.g + (Random.Range(0, 1.0f) * rangeG), AdjustedColor.b + (Random.Range(0, 1.0f) * rangeB), 1));

                                        //colors.Add(temp);
                                        _normals.Add(terrainHit.normal);
                                        i++;
                                    }
                                    else
                                    {// to not place everything at once, check if the first placed point far enough away from the last placed first one
                                        if (Vector3.Distance(terrainHit.point, _lastPosition) > brushSize)
                                        {
                                            var grassPosition = _hitPos;
                                            grassPosition -= this.transform.position;
                                            _positions.Add((grassPosition));
                                            _indicies.Add(i);
                                            _length.Add(new Vector2(sizeWidth, sizeLength));
                                            _colors.Add(new Color(AdjustedColor.r + (Random.Range(0, 1.0f) * rangeR), AdjustedColor.g + (Random.Range(0, 1.0f) * rangeG), AdjustedColor.b + (Random.Range(0, 1.0f) * rangeB), 1));
                                            _normals.Add(terrainHit.normal);
                                            i++;

                                            if (origin == Vector3.zero)
                                            {
                                                _lastPosition = _hitPos;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        e.Use();
                    }
                    // removing mesh points
                    if (toolbarInt == 1)
                    {
                        Ray ray = scene.camera.ScreenPointToRay(_mousePos);
                        if (Physics.Raycast(ray, out terrainHit, 200f, hitMask.value))
                        {
                            _hitPos = terrainHit.point;
                            hitPosGizmo = _hitPos;
                            hitNormal = terrainHit.normal;
                            for (int j = 0; j < _positions.Count; j++)
                            {
                                Vector3 pos = _positions[j];
                                pos += this.transform.position;
                                float dist = Vector3.Distance(terrainHit.point, pos);

                                // if its within the radius of the brush, remove all info
                                if (dist <= brushSize)
                                {
                                    _positions.RemoveAt(j);
                                    _colors.RemoveAt(j);
                                    _normals.RemoveAt(j);
                                    _length.RemoveAt(j);
                                    _indicies.RemoveAt(j);
                                    i--;

                                }
                            }
                            for (int ii = 0; ii < _indicies.Count; ii++)
                            {
                                _indicies[ii] = ii;
                            }
                        }
                        e.Use();
                    }
                    //edit
                    if (toolbarInt == 2)
                    {
                        Ray ray = scene.camera.ScreenPointToRay(_mousePos);

                        if (Physics.Raycast(ray, out terrainHit, 200f, hitMask.value))
                        {
                            _hitPos = terrainHit.point;
                            hitPosGizmo = _hitPos;
                            hitNormal = terrainHit.normal;
                            for (int j = 0; j < _positions.Count; j++)
                            {
                                Vector3 pos = _positions[j];

                                pos += this.transform.position;
                                float dist = Vector3.Distance(terrainHit.point, pos);

                                // if its within the radius of the brush, remove all info
                                if (dist <= brushSize)
                                {

                                    float falloff = Mathf.Clamp01((dist / (brushFalloffSize * brushSize)));

                                    //store the original color
                                    Color OrigColor = _colors[j];

                                    // add in the new color
                                    Color newCol = (new Color(AdjustedColor.r + (Random.Range(0, 1.0f) * rangeR), AdjustedColor.g + (Random.Range(0, 1.0f) * rangeG), AdjustedColor.b + (Random.Range(0, 1.0f) * rangeB), 1));

                                    Vector2 origLength = _length[j];
                                    Vector2 newLength = new Vector2(sizeWidth, sizeLength); ;

                                    _flowTimer++;
                                    if (_flowTimer > Flow)
                                    {
                                        // edit colors
                                        if (toolbarIntEdit == 0 || toolbarIntEdit == 2)
                                        {
                                            _colors[j] = Color.Lerp(newCol, OrigColor, falloff);
                                        }
                                        // edit grass length
                                        if (toolbarIntEdit == 1 || toolbarIntEdit == 2)
                                        {
                                            _length[j] = Vector2.Lerp(newLength, origLength, falloff);
                                        }
                                        _flowTimer = 0;
                                    }
                                }
                            }
                        }
                        e.Use();
                    }

                    // Reproject mesh points
                    if (toolbarInt == 3)
                    {
                        Ray ray = scene.camera.ScreenPointToRay(_mousePos);

                        if (Physics.Raycast(ray, out terrainHit, 200f, hitMask.value))
                        {
                            _hitPos = terrainHit.point;
                            hitPosGizmo = _hitPos;
                            hitNormal = terrainHit.normal;

                            for (int j = 0; j < _positions.Count; j++)
                            {
                                Vector3 pos = _positions[j];
                                pos += this.transform.position;
                                float dist = Vector3.Distance(terrainHit.point, pos);

                                // if its within the radius of the brush, raycast to a new position
                                if (dist <= brushSize)
                                {
                                    RaycastHit raycastHit;
                                    Vector3 meshPoint = new Vector3(pos.x, pos.y + reprojectOffset, pos.z);
                                    if (Physics.Raycast(meshPoint, Vector3.down, out raycastHit, 200f, paintMask.value))
                                    {
                                        Vector3 newPoint = raycastHit.point - this.transform.position;
                                        _positions[j] = newPoint;
                                    }
                                }
                            }
                        }
                        e.Use();
                    }
                    RebuildMesh();
                }
            }
        }
    }

    public void RebuildMesh()
    {
        if (mesh == null)
        {
            mesh = new Mesh();
        }
        mesh.Clear();
        mesh.SetVertices(_positions);
        _indi = _indicies.ToArray();
        mesh.SetIndices(_indi, MeshTopology.Points, 0);
        mesh.SetUVs(0, _length);
        mesh.SetColors(_colors);
        mesh.SetNormals(_normals);
        mesh.RecalculateBounds();
        _filter.sharedMesh = mesh;
    }
#endif
}
