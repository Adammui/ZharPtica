using UnityEngine;
using UnityEngine.Events;

namespace BraidGirl.Health
{
    public abstract class BaseHealthController : MonoBehaviour
    {
        [SerializeField] protected Health _health;

        protected bool _isDead;
        protected UnityEvent _onDeath;
        protected UnityEvent<Vector3> _onDamage;

        private void Awake()
        {
            _health.Init();
        }

        public void Init(UnityEvent onDeath, UnityEvent<Vector3> onDamage = null)
        {
            _onDeath = onDeath;
            _onDamage = onDamage;
        }

        /// <summary>
        /// Наносит урон value персонажу. Отталкивает в противоположную сторону от enemyPosition (если это необходимо)
        /// </summary>
        /// <param name="value">Значение наносимого урона</param>
        /// <param name="enemyPosition">Позиция наносящего урон</param>
        public abstract void Damage(float value, Vector3 enemyPosition);
    }
}
