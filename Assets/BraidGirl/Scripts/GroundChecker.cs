using UnityEngine;

namespace BraidGirl
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private Vector3 _size;
        [SerializeField] private LayerMask _ground;

        public bool Execute()
        {
            return Physics.CheckBox(transform.position, _size,Quaternion.identity, _ground);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position, _size);
        }
    }
}
