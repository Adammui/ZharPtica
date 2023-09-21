using UnityEngine;

namespace BraidGirl.Health
{
    public class HealthController : BaseHealthController
    {
        public override void Damage(float value, Vector3 enemyPosition)
        {
            if (!_isDead)
            {
                _onDamage?.Invoke(enemyPosition);
                if (_health.Damage(value) <= 0)
                {
                    _isDead = true;
                    _onDeath?.Invoke();
                }
            }
        }

        /// <summary>
        /// Возрождает персонажа, полностью восстанавливая ему здоровье
        /// </summary>
        public void Revive()
        {
            if (_isDead)
            {
                _health.Revive();
                _isDead = false;
            }
        }

        public void OnDeath()
        {
            Debug.Log("Death animation");
        }
    }
}
