using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RenderTerrainMap : MonoBehaviour
{

    public Camera camToDrawWith;
    // layer to render
    [SerializeField]
    private LayerMask _layer;
    // objects to render
    [SerializeField]
    private Renderer[] _renderers;
    // unity terrain to render
    [SerializeField]
    private Terrain[] _terrains;
    // map resolution
    public int resolution = 512;

    // padding the total size
    public float adjustScaling = 2.5f;
    // update texture every frame
    [SerializeField]
    private bool _realTimeDiffuse;
    private RenderTexture _tempTex;

    private Bounds _bounds;
    // resolution of the map

    // Start is called before the first frame update

    public void GetBounds()
    {
        _bounds = new Bounds(transform.position, Vector3.zero);
        if (_renderers.Length > 0)
        {
            foreach (Renderer renderer in _renderers)
            {
                _bounds.Encapsulate(renderer.bounds);
            }
        }

        if (_terrains.Length > 0)
        {
            foreach (Terrain terrain in _terrains)
            {
                _bounds.Encapsulate(terrain.terrainData.bounds);
            }
        }
    }

    public void OnEnable()
    {
        _tempTex = new RenderTexture(resolution, resolution, 24);
        GetBounds();
        SetUpCam();
        DrawDiffuseMap();
    }


    public void Start()
    {
        GetBounds();
        SetUpCam();
        DrawDiffuseMap();
    }

    public void OnRenderObject()
    {
        if (!_realTimeDiffuse)
        {
            return;
        }
        UpdateTex();
    }

    public void UpdateTex()
    {
        camToDrawWith.enabled = true;
        camToDrawWith.targetTexture = _tempTex;
        Shader.SetGlobalTexture("_TerrainDiffuse", _tempTex);
    }
    public void DrawDiffuseMap()
    {
        DrawToMap("_TerrainDiffuse");
    }

    public void DrawToMap(string target)
    {
        camToDrawWith.enabled = true;
        camToDrawWith.targetTexture = _tempTex;
        camToDrawWith.depthTextureMode = DepthTextureMode.Depth;
        Shader.SetGlobalFloat("_OrthographicCamSize", camToDrawWith.orthographicSize);
        Shader.SetGlobalVector("_OrthographicCamPos", camToDrawWith.transform.position);
        camToDrawWith.Render();
        Shader.SetGlobalTexture(target, _tempTex);
        camToDrawWith.enabled = false;
    }

    public void SetUpCam()
    {
        if (camToDrawWith == null)
        {
            camToDrawWith = GetComponentInChildren<Camera>();
        }
        float size = _bounds.size.magnitude;
        camToDrawWith.cullingMask = _layer;
        camToDrawWith.orthographicSize = size / adjustScaling;
        camToDrawWith.transform.parent = null;
        camToDrawWith.transform.position = _bounds.center + new Vector3(0, _bounds.extents.y + 5f, 0);
        camToDrawWith.transform.parent = gameObject.transform;
    }

}
