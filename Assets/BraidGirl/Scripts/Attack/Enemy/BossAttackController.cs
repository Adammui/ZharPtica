using BraidGirl.AI;
using BraidGirl.Scripts.AI.Attack.Abstract;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack
{
    public class BossAttackController : BaseActiveAttackController
    {
        private PlayerFinder _playerFinder;
        private BaseProjectileAttack _projectileAttack;

        public override void Init(GameObject gameObject)
        {
            _playerFinder = gameObject.GetComponent<PlayerFinder>();
            _baseAttacks.Add(gameObject.GetComponent<DashAttack>());
            _projectileAttack = gameObject.GetComponent<FeatherAttack>();
            ((DashAttack)_baseAttacks[0]).Init(ResetAttack);
            _projectileAttack.Init(ResetAttack);
        }

        public override void Execute()
        {
            if (_playerFinder.IsPlayerInArea(FinderType.Attack) && !_isAttacking)
            {
                _isAttacking = true;
                Vector3 direction = _playerFinder.PlayerPosition.transform.position;
                direction.z = 0;
                ((DashAttack)_baseAttacks[0]).Attack(direction);
            }
            else if (!_isAttacking)
            {
                _isAttacking = true;
                ((FeatherAttack)_projectileAttack).Attack();
            }
        }

        protected override void ResetAttack()
        {
            _isAttacking = false;
        }
    }
}
