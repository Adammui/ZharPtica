using BraidGirl.AI;
using BraidGirl.Scripts.AI.Attack.Abstract;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack
{
    public class AttackController : BaseActiveAttackController
    {
        private PlayerFinder _playerFinder;

        public override void Init(GameObject gameObject)
        {
            _baseAttacks.Add(gameObject.GetComponent<DashAttack>());
            _playerFinder = gameObject.GetComponent<PlayerFinder>();
            ((DashAttack)_baseAttacks[0]).Init(ResetAttack);
        }

        public override void Execute()
        {
            if (!_isAttacking)
            {
                _isAttacking = true;
                Vector3 direction = _playerFinder.PlayerPosition.transform.position;
                direction.z = 0;
                ((DashAttack)_baseAttacks[0]).Attack(direction);
            }
        }

        protected override void ResetAttack()
        {
            _isAttacking = false;
        }
    }
}
