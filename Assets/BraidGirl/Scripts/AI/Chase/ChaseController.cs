using BraidGirl.Abstract;
using BraidGirl.AI.Movement;
using BraidGirl.AI.Rotation;
using UnityEngine;

namespace BraidGirl.AI.Chase
{
    public class ChaseController : IExecute, IInitialization
    {
        private BaseMovementController _movementController;
        private RotationController _rotationController;
        private PlayerFinder _playerFinder;

        public void Init(GameObject gameObject)
        {
            _movementController = gameObject.GetComponent<AIMovementController>();
            _rotationController = gameObject.GetComponent<RotationController>();
            _playerFinder = gameObject.GetComponent<PlayerFinder>();
        }

        public void Execute()
        {
            Vector3 direction = _playerFinder.PlayerPosition.transform.position;
            _rotationController.Execute(direction);
            _movementController.Execute(direction);
        }
    }
}
