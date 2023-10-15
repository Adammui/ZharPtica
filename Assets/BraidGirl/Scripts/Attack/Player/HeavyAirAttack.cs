using System.Collections;
using BraidGirl.Attack;
using BraidGirl.Health;
using UnityEngine;

namespace BraidGirl.Scripts.Attack.Player
{
    /// <summary>
    /// Выполнение сильной атаки в воздухе
    /// </summary>
    public class HeavyAirAttack : BaseAttack
    {
        [SerializeField]
        private float _duration;

        [SerializeField] private float _gravityMultiplier;

        private CharacterMovement _movement;
        private float _defaultGravity;
        private int _attackHash;
        private Input _inputController;
        private bool _isAttack;

        private new void Start()
        {
            _inputController = GetComponent<Input>();
            _movement = GetComponent<CharacterMovement>();
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

            _defaultGravity = _movement.Gravity;
            _movement.Gravity *= _gravityMultiplier;

            while (!_inputController.IsGrounded)
                yield return new WaitForFixedUpdate();

            WeaponCollider.SetActive(true);
            yield return new WaitForSeconds(_duration);
            ResetAttack();
        }

        /// <summary>
        /// Сброс атаки
        /// </summary>
        private void ResetAttack()
        {
            WeaponCollider.SetActive(false);
            _movement.Gravity = _defaultGravity;
        }
    }
}
