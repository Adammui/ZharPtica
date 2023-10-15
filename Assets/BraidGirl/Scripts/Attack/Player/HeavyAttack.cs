using System;
using System.Collections;
using BraidGirl.Health;
using UnityEngine;

namespace BraidGirl.Attack
{
    /// <summary>
    /// Выполнение сильной атаки
    /// </summary>
    public class HeavyAttack : BaseAttack
    {
        [SerializeField]
        private float _duration;

        private Action _onResetAttack;
        private int _attackHash;
        private WaitForSeconds _waitDuration;

        private new void Start()
        {
            base.Start();
            _waitDuration = new WaitForSeconds(_duration);
            _attackHash = Animator.StringToHash("triggerRangedAttack");
        }

        protected override void HandleAttack(GameObject enemy)
        {
            if (enemy.TryGetComponent(out BaseHealthController health))
                health.Damage(Damage, transform.position);
        }

        public override IEnumerator Attack()
        {
            Animator.SetTrigger(_attackHash);
            WeaponCollider.SetActive(true);

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
            WeaponCollider.SetActive(false);
        }
    }
}
