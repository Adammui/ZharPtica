using BraidGirl.Rotating;
using UnityEngine;

namespace BraidGirl.AI.Movement
{
    public class FeatherMoving : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void MoveAndRotate(Vector3 destination)
        {
            transform.rotation = Rotator.Rotate(destination);
            Vector3 currDestination = destination - transform.position;
            _rb.velocity = currDestination.normalized * _speed;
        }
    }
}
