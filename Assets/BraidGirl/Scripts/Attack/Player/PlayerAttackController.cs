using BraidGirl.Abstract;
using BraidGirl.Attack;
using BraidGirl.Scripts.AI.Attack.Abstract;
using UnityEngine;

namespace BraidGirl.Scripts.Attack.Player
{
    /// <summary>
    /// Контроллер атаки игрока
    /// </summary>
    public class PlayerAttackController : IExecute, IInitialization
    {
        /// <summary>
        /// Нумерация атак игрока
        /// </summary>
        private struct AttackType
        {
            public const int lightAttack = 0;
            public const int heavyGroundAttack = 1;
            public const int heavyAirAttack = 2;
        }

        private bool _canAttack = true;
        private bool _isAttacking;

        private Input _inputController;

        private IAttack _lightAttack;
        private IAttack _groundHeavyAttack;
        private IAttack _airHeavyAttack;

        private PlayerAttackView _playerAttackView;

        public bool IsAttacking => _isAttacking;

        public void Init(GameObject player)
        {
            _inputController = player.GetComponent<Input>();

            _lightAttack = player.GetComponent<LightAttack>();
            _groundHeavyAttack = player.GetComponent<HeavyAttack>();
            _airHeavyAttack = player.GetComponent<HeavyAirAttack>();
            _playerAttackView = player.GetComponent<PlayerAttackView>();

            AttackTransitionBehaviour.s_onResetAttack = ResetAttack;
        }

        /// <summary>
        /// Попытаться выполнить атаку
        /// </summary>
        public void Execute()
        {
            if (_canAttack && !_isAttacking)
            {
                _canAttack = false;
                _isAttacking = true;

                if (_inputController.IsLightAttack)
                {
                    _playerAttackView.Activate(AttackType.lightAttack);
                    _lightAttack.Attack();
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
            if (_inputController.IsGrounded)
            {
                _playerAttackView.Activate(AttackType.heavyGroundAttack);
                _groundHeavyAttack.Attack();
            }
            else
            {
                _playerAttackView.Activate(AttackType.heavyAirAttack);
                _airHeavyAttack.Attack();
            }
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
