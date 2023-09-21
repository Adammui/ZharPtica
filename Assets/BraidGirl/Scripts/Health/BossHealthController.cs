using BraidGirl.Health;
using UnityEngine;

namespace BraidGirl.Scripts.Health
{
    public class BossHealthController : BaseHealthController
    {
        [SerializeField] private float _minHealthChangeDamageType;

        public override void Damage(float value, Vector3 enemyPosition)
        {
            if (!_health.IsHealthLess(_minHealthChangeDamageType) && !_isDead)
                _health.Damage(value);
        }

        /// <summary>
        /// Наносит персонажу урон при столкновении со входом в пещеру
        /// </summary>
        /// <param name="value">Значение наносимого урона</param>
        public void Damage(float value)
        {
            if (_isDead) return;

            if (_health.IsHealthLess(_minHealthChangeDamageType))
            {
                _health.Kill();
                _isDead = true;
                _onDeath.Invoke();
            }
            else
            {
                _health.Damage(value);
            }
        }
    }
}
