using BraidGirl.AI.Movement;
using BraidGirl.Health;
using BraidGirl.Scripts.Push;
using UnityEngine;
using UnityEngine.Events;

namespace BraidGirl.AI
{
    public class EnemyController : MonoBehaviour
    {
        private EnemyAttackController _enemyAttackController;
        private MoveController _moveController;
        private BaseHealthController _healthController;
        private PushController _pushController;
        private UnityEvent<Vector3> _onDamage;
        private UnityEvent _onDeath;

        private void Awake()
        {
            _onDeath = new UnityEvent();
            _onDamage = new UnityEvent<Vector3>();
            _enemyAttackController = GetComponent<EnemyAttackController>();
            _moveController = GetComponent<MoveController>();
            _healthController = GetComponent<HealthController>();
            _pushController = GetComponent<PushController>();

            _onDamage.AddListener(_pushController.HandlePush);

            _onDeath.AddListener(_enemyAttackController.OnDeath);
            _onDeath.AddListener(_moveController.OnDeath);
            _onDeath.AddListener(((HealthController)_healthController).OnDeath);
            _onDeath.AddListener(_pushController.OnDeath);
            _healthController.Init(_onDeath, _onDamage);
        }
    }
}
