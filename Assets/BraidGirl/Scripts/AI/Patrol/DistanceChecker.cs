using UnityEngine;

namespace BraidGirl.Scripts.AI.Patrol
{
    public class DistanceChecker : MonoBehaviour
    {
        [SerializeField] private float _distance;

        public bool Check(Vector3 destination) => Vector3.SqrMagnitude(transform.position - destination) < _distance * _distance;
    }
}
