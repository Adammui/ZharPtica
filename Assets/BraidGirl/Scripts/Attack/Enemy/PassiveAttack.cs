using BraidGirl.Health;
using UnityEngine;
using BaseAttack = BraidGirl.Scripts.AI.Attack.Abstract.BaseAttack;

namespace BraidGirl.Scripts.AI.Attack
{
    public class PassiveAttack : BaseAttack
    {
        protected override void HandleAttack(GameObject enemy)
        {
            if (enemy.TryGetComponent(out BaseHealthController health))
                health.Damage(Damage, transform.position);
        }
    }
}

