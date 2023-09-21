using BraidGirl.Scripts.AI.Attack;
using BraidGirl.Scripts.Health;
using UnityEngine;
using UnityEngine.Events;

namespace BraidGirl.AI
{
    public class EnemyController_3 : MonoBehaviour
    {
        [SerializeField]
        private GameObject _caveEnter;

        private BossStateManager _bossStateManager;
        private BossAttackController _bossAttackController;
        private BossHealthController _healthController;
        private UnityEvent _onDeath;

        private void Awake()
        {
            _onDeath = new UnityEvent();

            _bossAttackController = new();
            _bossStateManager = new BossStateManager(_bossAttackController);
            _healthController = GetComponent<BossHealthController>();
        }

        private void Start()
        {
            InitControllers();
        }

        private void InitControllers()
        {
            _onDeath.AddListener(OnDeath);

            _bossAttackController.Init(gameObject);
            _healthController.Init(_onDeath);
        }

        private void Update()
        {
            _bossStateManager.Execute();
        }

        private void OnDeath()
        {
            Destroy(_caveEnter);
            enabled = false;
        }
    }
}
