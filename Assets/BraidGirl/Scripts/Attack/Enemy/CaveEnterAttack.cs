using BraidGirl.Scripts.Health;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack
{
    /// <summary>
    /// Нанесение урона противнику при столкновении с объектом
    /// </summary>
    public class CaveEnterAttack : PassiveAttack
    {
        protected override void HandleAttack(GameObject enemy)
        {
            if (enemy.TryGetComponent(out BossHealthController health))
                health.Damage(Damage);
        }
    }
}
