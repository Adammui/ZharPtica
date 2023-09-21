using BraidGirl.Abstract;
using BraidGirl.AI;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack
{
    public class AttackController : IExecute, IInitialization
    {
        private bool _isAttacking;
        private DashAttack _dashAttack;
        private PlayerFinder _playerFinder;

        public bool IsAttacking => _isAttacking;

        public void Init(GameObject gameObject)
        {
            _dashAttack = gameObject.GetComponent<DashAttack>();
            _playerFinder = gameObject.GetComponent<PlayerFinder>();
            _dashAttack.Init(Reset);
        }

        public void Execute()
        {
            if (!_isAttacking)
            {
                _isAttacking = true;
                Vector3 direction = _playerFinder.PlayerPosition.transform.position;
                direction.z = 0;
                _dashAttack.Attack(direction);
            }
        }

        private void Reset()
        {
            _isAttacking = false;
        }
    }
}
