using BraidGirl.Attack;
using UnityEngine;

namespace BraidGirl.Scripts.Attack.Player
{
    /// <summary>
    /// Контроллер атаки игрока
    /// </summary>
    public class PlayerAttackController
    {
        private bool _canAttack = true;
        private bool _isAttacking;

        private Input _inputController;

        private BaseAttack _lightAttack;
        private BaseAttack _groundHeavyAttack;
        private BaseAttack _airHeavyAttack;
        private MonoBehaviour _monoBehaviour;

        public bool IsAttacking => _isAttacking;

        public void Init(GameObject player)
        {
            _inputController = player.GetComponent<Input>();

            _lightAttack = player.GetComponent<LightAttack>();
            _groundHeavyAttack = player.GetComponent<HeavyAttack>();
            _airHeavyAttack = player.GetComponent<HeavyAirAttack>();

            _monoBehaviour = _lightAttack;
            AttackTransitionBehaviour.s_onResetAttack = ResetAttack;
        }

        /// <summary>
        /// Попытаться выполнить атаку
        /// </summary>
        public void TryAttack()
        {
            if (_canAttack && !_isAttacking)
            {
                _canAttack = false;
                _isAttacking = true;

                if (_inputController.IsLightAttack)
                {
                    _monoBehaviour.StartCoroutine(_lightAttack.Attack());
                }
                else if (_inputController.IsHeavyAttack)
                {
                    HandleHeavyAttack();
                }
            }
        }

        /// <summary>
        /// Определение типа сильной атаки
        /// </summary>
        private void HandleHeavyAttack()
        {
            _monoBehaviour.StartCoroutine(_inputController.IsGrounded
                ? _groundHeavyAttack.Attack()
                : _airHeavyAttack.Attack());
        }

        /// <summary>
        /// Сброс атаки
        /// </summary>
        private void ResetAttack()
        {
            _canAttack = true;
            _isAttacking = false;
        }
    }
}
