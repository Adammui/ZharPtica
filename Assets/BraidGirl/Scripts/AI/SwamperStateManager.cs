using BraidGirl.Health;
using BraidGirl.Scripts.AI.Patrol;
using BraidGirl.Scripts.Push;
using UnityEngine;
using UnityEngine.Events;

namespace BraidGirl.AI
{
    public class SwamperStateManager : MonoBehaviour
    {
        private HealthController _healthController;
        private PushController _pushController;
        private EnemyAttackController _enemyAttackController;

        private PatrolController _patrolController;

        private UnityEvent _onDeath;
        private UnityEvent<Vector3> _onDamage;


        private void Awake()
        {
            _onDeath = new UnityEvent();
            _onDamage = new UnityEvent<Vector3>();

            _healthController = GetComponent<HealthController>();
            _pushController = GetComponent<PushController>();
            _enemyAttackController = GetComponent<EnemyAttackController>();
            _patrolController = new PatrolController();
            InitControllers();
        }

        private void InitControllers()
        {
            _onDeath.AddListener(OnDeath);
            _onDeath.AddListener(_healthController.OnDeath);
            _onDeath.AddListener(_enemyAttackController.OnDeath);

            _onDamage.AddListener(_pushController.HandlePush);

            _healthController.Init(_onDeath, _onDamage);
            _patrolController.Init(gameObject);
        }

        public void Update()
        {
            _patrolController.Execute();
        }

        private void OnDeath()
        {
            enabled = false;
        }
    }
}
