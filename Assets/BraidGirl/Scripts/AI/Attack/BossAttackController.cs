using BraidGirl.Abstract;
using BraidGirl.AI;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack
{
    public class BossAttackController : IExecute, IInitialization
    {
        private PlayerFinder _playerFinder;

        private DashAttack _dashAttack;
        private FeatherAttack _featherAttack;
        private bool _isAttacking;
        private MonoBehaviour _monoBehaviour;

        public void Init(GameObject gameObject)
        {
            _playerFinder = gameObject.GetComponent<PlayerFinder>();
            _dashAttack = gameObject.GetComponent<DashAttack>();
            _featherAttack = gameObject.GetComponent<FeatherAttack>();
            _monoBehaviour = gameObject.GetComponent<MonoBehaviour>();
            _dashAttack.Init(ResetAttack);
            _featherAttack.Init(ResetAttack);
        }

        public void Execute()
        {
            if (_playerFinder.IsPlayerInArea(FinderType.Attack) && !_isAttacking)
            {
                _isAttacking = true;
                Vector3 direction = _playerFinder.PlayerPosition.transform.position;
                direction.z = 0;
                _dashAttack.Attack(direction);
            }
            else if (!_isAttacking)
            {
                _isAttacking = true;
                // Vector3 direction = _playerFinder.PlayerPosition.transform.position;
                // direction.z = 0;
                _monoBehaviour.StartCoroutine(_featherAttack.Attack());
            }
        }

        private void ResetAttack()
        {
            _isAttacking = false;
        }
    }
}
