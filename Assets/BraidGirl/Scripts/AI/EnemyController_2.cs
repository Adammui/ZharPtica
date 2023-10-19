using BraidGirl.AI.Chase;
using BraidGirl.AI.Movement;
using BraidGirl.Health;
using BraidGirl.Scripts.AI.Attack;
using BraidGirl.Scripts.AI.Patrol;
using UnityEngine;
using UnityEngine.Events;

namespace BraidGirl.AI
{
    public class EnemyController_2 : MonoBehaviour
    {
        private StateManager _stateManager;
        private PatrolController _patrolController;
        private AttackController _attackController;
        private ChaseController _chaseController;
        private BaseHealthController _healthController;

        private UnityEvent _onDeath;

        private void Start()
        {
            _healthController = GetComponent<HealthController>();
            _onDeath = new UnityEvent();

            _patrolController = new PatrolController();
            _attackController = new AttackController();
            _chaseController = new ChaseController();
            _stateManager = new StateManager(_patrolController, _attackController, _chaseController);
            InitControllers();
        }

        private void InitControllers()
        {
            _onDeath.AddListener(OnDeath);

            _healthController.Init(_onDeath);
            _patrolController.Init(gameObject);
            _chaseController.Init(gameObject);
            _attackController.Init(gameObject);
            _stateManager.Init(gameObject);
        }

        private void Update()
        {
            _stateManager.Execute();
        }

        private void OnDeath()
        {
            enabled = false;
        }
    }
}
