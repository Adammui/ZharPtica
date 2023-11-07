using System.Collections;
using BraidGirl.Health;
using BraidGirl.Scripts.AI.Attack.Abstract;
using UnityEngine;

namespace BraidGirl.Scripts.Attack.Player
{
    /// <summary>
    /// Выполнение слабой атаки
    /// </summary>
    public class LightAttack : BaseAttack, IAttack
    {
        [SerializeField]
        private float _duration;
        [SerializeField]
        private GameObject _weaponCollider;

        private WaitForSeconds _waitDuration;

        private new void Start()
        {
            base.Start();
            _waitDuration = new WaitForSeconds(_duration);
        }

        protected override void HandleAttack(GameObject enemy)
        {
            if (enemy.TryGetComponent(out BaseHealthController health))
                health.Damage(Damage, transform.position);
        }

        public void Attack()
        {
            StartCoroutine(Activate());
        }

        private IEnumerator Activate()
        {
            _weaponCollider.SetActive(true);
#if UNITY_EDITOR
            yield return new WaitForSeconds(_duration);
#else
            yield return _waitDuration;
#endif
            ResetAttack();
        }

        /// <summary>
        /// Сброс атаки
        /// </summary>
        private void ResetAttack()
        {
            _weaponCollider.SetActive(false);
        }
    }
}
