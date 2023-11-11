using UnityEngine;
using UnityEngine.AI;

namespace BraidGirl.AI.Movement
{
    public class NavmeshMovement : MonoBehaviour, IMovement
    {
        [SerializeField]
        private float _speed;

        private NavMeshAgent _agent;

        float IMovement.Speed => _speed;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _speed;
        }

        public void Move(Vector3 destination)
        {
            _agent.velocity = destination.normalized * _speed;
        }

        public void OnDeath()
        {
            _agent.velocity = Vector3.zero;
            enabled = false;
        }
    }
}
