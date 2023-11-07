using BraidGirl.AI.Rotation.Abstract;
using UnityEngine;

namespace BraidGirl.AI.Rotation
{
    public class RotationController : MonoBehaviour
    {
        private IRotation _rotation;

        private void Awake()
        {
            _rotation = GetComponent<AIRotation>();
        }

        public void Execute(Vector3 destination)
        {
            Vector3 currDestination = destination - transform.position;
            Vector3 lookPos = new (currDestination.x, 0, currDestination.z);
            _rotation.Rotate(lookPos);
        }
    }
}
