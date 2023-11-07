using UnityEngine;

namespace BraidGirl.Scripts.AI.Patrol
{
    public class DistanceChecker : MonoBehaviour
    {
        [SerializeField] private float _distance;

        public bool Check(Vector3 destination)
        {
            Vector3 transformWithoutZ = transform.position;
            transformWithoutZ.z = 0;
            destination.z = 0;
            return Vector3.SqrMagnitude(transformWithoutZ - destination) < _distance * _distance;
        }
    }
}
