using BraidGirl.Abstract;
using BraidGirl.AI.Movement;
using UnityEngine;

namespace BraidGirl.AI.Chase
{
    public class ChaseController : IExecute, IInitialization
    {
        private Moving _movement;
        private PlayerFinder _playerFinder;

        public void Init(GameObject gameObject)
        {
            _movement = gameObject.GetComponent<Moving>();
            _playerFinder = gameObject.GetComponent<PlayerFinder>();
        }

        public void Execute()
        {
            Vector3 direction = _playerFinder.PlayerPosition.transform.position;
            direction.z = 0;
            _movement.MoveAndRotate(direction);
        }
    }
}
