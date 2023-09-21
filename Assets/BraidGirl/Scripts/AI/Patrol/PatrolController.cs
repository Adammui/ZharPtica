using BraidGirl.Abstract;
using BraidGirl.AI.Movement;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Patrol
{
    public class PatrolController : IExecute, IInitialization
    {
        private Patrol _patrol;
        private DistanceChecker _distanceChecker;
        private Moving _movement;
        private Vector3 _destination;

        public void Init(GameObject gameObject)
        {
            _patrol = gameObject.GetComponent<Patrol>();
            _movement = gameObject.GetComponent<Moving>();
            _distanceChecker = gameObject.GetComponent<DistanceChecker>();
            _destination = _patrol.GetNextPoint();
        }

        public void Execute()
        {
            if (_distanceChecker.Check(_destination))
                _destination = _patrol.GetNextPoint();
            _movement.MoveAndRotate(_destination);
        }
    }
}
