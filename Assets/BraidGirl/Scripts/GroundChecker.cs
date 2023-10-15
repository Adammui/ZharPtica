using UnityEngine;

namespace BraidGirl
{
    /// <summary>
    /// Проверка нахождения объекта на земле ground
    /// </summary>
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private Vector3 _size;
        [SerializeField] private LayerMask _ground;

        /// <summary>
        /// Проверка нахождения текущего объекта на земле
        /// </summary>
        /// <returns></returns>
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
