using BraidGirl.AI.Chase;
using BraidGirl.Health;
using BraidGirl.Scripts.AI.Attack;
using BraidGirl.Scripts.AI.Patrol;
using UnityEngine;
using UnityEngine.Events;

namespace BraidGirl.AI
{
    //Change name on stateManager, что позволит без конфликтов использовать тут controllers и заменить их на один класс/список/массив, содержащий все контроллеры
    public class ChasingMonsterStateManager : MonoBehaviour
    {
        private PatrolController _patrolController;
        private AttackController _attackController;
        private ChaseController _chaseController;
        private BaseHealthController _healthController;

        private PlayerFinder _playerFinder;
        private UnityEvent _onDeath;

        private bool _canChase;
        private bool _canAttack;

        private void Start()
        {
            _healthController = GetComponent<HealthController>();
            _playerFinder = gameObject.GetComponent<PlayerFinder>();
            _onDeath = new UnityEvent();

            _patrolController = new PatrolController();
            _attackController = new AttackController();
            _chaseController = new ChaseController();
            InitControllers();
        }

        private void InitControllers()
        {
            _onDeath.AddListener(OnDeath);

            _healthController.Init(_onDeath);
            _patrolController.Init(gameObject);
            _chaseController.Init(gameObject);
            _attackController.Init(gameObject);
        }

        private void Update()
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

        private void OnDeath()
        {
            enabled = false;
        }
    }
}
