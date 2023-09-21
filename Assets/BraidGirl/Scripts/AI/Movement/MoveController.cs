using BraidGirl.Scripts.AI.Patrol;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace BraidGirl.AI.Movement
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _distance;

        private NavMeshAgent _agent;
        private Patrol _patrol;
        private Vector3 _destination;
        private bool _setPoint;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _patrol = GetComponent<Patrol>();
            _destination = _patrol.GetNextPoint();
            _agent.speed = _speed;
        }

        private void Update()
        {
            RotateOnPoint();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void RotateOnPoint()
        {
            var lookPos = _destination;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);
        }

        private void Move()
        {
            if (Vector3.SqrMagnitude(transform.position - _destination) < _distance * _distance)
                _destination = _patrol.GetNextPoint() - transform.position;
            else
                _agent.velocity = _destination.normalized * _speed;
        }

        public void OnDeath()
        {
            _agent.velocity = Vector3.zero;
            enabled = false;
        }
    }
}
