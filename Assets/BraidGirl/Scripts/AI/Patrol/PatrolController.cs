using BraidGirl.Abstract;
using BraidGirl.AI.Movement;
using BraidGirl.AI.Rotation;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Patrol
{
    public class PatrolController : IExecute, IInitialization
    {
        private Patrol _patrol;
        private DistanceChecker _distanceChecker;
        private BaseMovementController _movementController;
        private RotationController _rotationController;
        private Vector3 _destination;

        public void Init(GameObject gameObject)
        {
            _patrol = gameObject.GetComponent<Patrol>();
            _movementController = gameObject.GetComponent<AIMovementController>();
            _rotationController = gameObject.GetComponent<RotationController>();
            _distanceChecker = gameObject.GetComponent<DistanceChecker>();
            _destination = _patrol.GetNextPoint();
        }

        public void Execute()
        {
            if (_distanceChecker.Check(_destination))
                _destination = _patrol.GetNextPoint();
            _rotationController.Execute(_destination);
            _movementController.Execute(_destination);
        }
    }
}
