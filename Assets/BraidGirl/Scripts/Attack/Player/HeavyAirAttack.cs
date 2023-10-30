using System.Collections;
using BraidGirl.Health;
using BraidGirl.Scripts.AI.Attack.Abstract;
using UnityEngine;

namespace BraidGirl.Scripts.Attack.Player
{
    /// <summary>
    /// Выполнение сильной атаки в воздухе
    /// </summary>
    public class HeavyAirAttack : BaseAttack, IAttack
    {
        [SerializeField]
        private float _duration;
        [SerializeField]
        private GameObject _weaponCollider;
        [SerializeField]
        private float _gravityMultiplier;

        private CharacterMovement _movement;
        private float _defaultGravity;
        private Input _inputController;
        private bool _isAttack;

        private new void Start()
        {
            _inputController = GetComponent<Input>();
            _movement = GetComponent<CharacterMovement>();
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
            _defaultGravity = _movement.Gravity;
            _movement.Gravity *= _gravityMultiplier;

            while (!_inputController.IsGrounded)
                yield return new WaitForFixedUpdate();

            _weaponCollider.SetActive(true);
            yield return new WaitForSeconds(_duration);
            ResetAttack();

        }

        /// <summary>
        /// Сброс атаки
        /// </summary>
        private void ResetAttack()
        {
            _weaponCollider.SetActive(false);
            _movement.Gravity = _defaultGravity;
        }
    }
}
