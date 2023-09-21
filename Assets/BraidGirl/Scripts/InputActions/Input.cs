using UnityEngine;
using UnityEngine.InputSystem;

namespace BraidGirl
{
    public class Input : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private bool _isMovementPressed;
        private bool _isDashPressed;
        private bool _isJumpPressed;
        private bool _isAttractPressed;
        private bool _isGrounded;
        private bool _isLightAttack;
        private bool _isHeavyAttack;
        private Vector2 _currentMovementInput;

        public bool IsMovementPressed => _isMovementPressed;
        public bool IsDashPressed => _isDashPressed;
        public bool IsJumpPressed => _isJumpPressed;
        public bool IsAttractPressed => _isAttractPressed;
        public bool IsLightAttack => _isLightAttack;
        public bool IsHeavyAttack => _isHeavyAttack;

        public bool IsGrounded
        {
            get => _isGrounded;
            set => _isGrounded = value;
        }

        public Vector2 CurrentMovementInput => _currentMovementInput;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            SubscribeCallbacks();
        }

        private void SubscribeCallbacks()
        {
            _playerInput.Character.Move.started += Movement;
            _playerInput.Character.Move.canceled += Movement;
            _playerInput.Character.Move.performed += Movement;
            _playerInput.Character.Jump.started += Jump;
            _playerInput.Character.Jump.canceled += Jump;
            _playerInput.Character.Dash.started += Dash;
            _playerInput.Character.Dash.canceled += Dash;

            _playerInput.Character.Attack.started += LightAttack;
            _playerInput.Character.Attack.canceled += LightAttack;

            _playerInput.Character.HeavyAttack.started += HeavyAttack;
            _playerInput.Character.HeavyAttack.canceled += HeavyAttack;

            _playerInput.Character.Attract.started += Attract;
            _playerInput.Character.Attract.canceled += Attract;
        }

        private void Movement(InputAction.CallbackContext context)
        {
            _currentMovementInput = context.ReadValue<Vector2>();
            _isMovementPressed = CurrentMovementInput.x != 0 || CurrentMovementInput.y != 0;
        }

        private void Jump(InputAction.CallbackContext context)
        {
            _isJumpPressed = context.ReadValueAsButton();
        }

        private void Dash(InputAction.CallbackContext context)
        {
            _isDashPressed = context.ReadValueAsButton();
        }

        private void LightAttack(InputAction.CallbackContext context)
        {
            _isLightAttack = context.ReadValueAsButton();
        }

        private void HeavyAttack(InputAction.CallbackContext context)
        {
            _isHeavyAttack = context.ReadValueAsButton();
        }

        private void Attract(InputAction.CallbackContext obj)
        {
            _isAttractPressed = obj.ReadValueAsButton();
        }

        private void OnEnable()
        {
            _playerInput.Character.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Character.Disable();
        }

    }
}
