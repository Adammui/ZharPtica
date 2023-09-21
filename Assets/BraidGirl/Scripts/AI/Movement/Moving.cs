using BraidGirl.Rotating;
using UnityEngine;
using UnityEngine.AI;

namespace BraidGirl.AI.Movement
{
    public class Moving : MonoBehaviour //добавить интерфейс + мост, вызывать запуск анимаций
    {
        [SerializeField] private float _speed;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void MoveAndRotate(Vector3 destination)
        {
            transform.rotation = Rotator.Rotate(destination);
            Vector3 transformWithZ = transform.position;
            transformWithZ.z = 0;
            Vector3 currDestination = destination - transformWithZ;
            _agent.velocity = currDestination.normalized * _speed;
        }
    }
}
