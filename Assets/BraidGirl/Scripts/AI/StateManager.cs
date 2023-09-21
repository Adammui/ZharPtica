using BraidGirl.Abstract;
using BraidGirl.AI.Chase;
using BraidGirl.Scripts.AI.Attack;
using BraidGirl.Scripts.AI.Patrol;
using UnityEngine;

namespace BraidGirl.AI
{
    public class StateManager : IExecute, IInitialization
    {
        private PatrolController _patrolController;
        private AttackController _attackController;
        private ChaseController _chaseController;
        private PlayerFinder _playerFinder;
        private bool _canChase;
        private bool _canAttack;

        public StateManager(PatrolController patrolController,
            AttackController attackController,
            ChaseController chaseController)
        {
            _patrolController = patrolController;
            _attackController = attackController;
            _chaseController = chaseController;
        }

        public void Init(GameObject gameObject)
        {
            _playerFinder = gameObject.GetComponent<PlayerFinder>();
        }

        public void Execute()
        {
            _canChase = _playerFinder.IsPlayerInArea(FinderType.Chase);
            _canAttack = _playerFinder.IsPlayerInArea(FinderType.Attack);

            if (_canChase && _canAttack)
            {
                _attackController.Execute();
            }
            else if (!_canAttack && _canChase && !_attackController.IsAttacking)
            {
                _chaseController.Execute();
            }
            else if(!_attackController.IsAttacking)
            {
                _patrolController.Execute();
            }
        }
    }
}
