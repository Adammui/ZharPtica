using UnityEngine;

namespace BraidGirl
{
    /// <summary>
    /// Выполнение прыжка, гравитации и перемещения
    /// </summary>
    public class CharacterMovement : MonoBehaviour
    {
        // Declare reference variables
        [Header("Movement")]
        [SerializeField]
        private Player _player;
        [SerializeField]
        private float _characterSpeed = 3.0f;

        // Gravity variable
        [SerializeField]
        private float _gravity = -9.8f;
        [SerializeField]
        private float _groundedGravity = -.05f;

        private Vector3 _currentMovement;
        private Vector3 _appliedMovement;

        private CharacterController _characterController;
        private Input _playerInput;
        private float _zero = 0.0f;

        /// <summary>
        /// Текущее значение гравитации игрока
        /// </summary>
        public float Gravity
        {
            get => _gravity;
            set => _gravity = value;
        }

        //Rotation variable
        private float _rotationVar;

        // Jumping variable
        [Header("Jump")]
        [SerializeField]
        private float _jumpHeight = 2.0f;
        [SerializeField]
        private float _maxJumpTime = 2.0f;
        private float _initialJumpVelocity;
        private bool _isJumping = false;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerInput = GetComponent<Input>();
            SetupJumpVariables();
        }

        /// <summary>
        /// Задает переменные прыжка
        /// </summary>
        private void SetupJumpVariables()
        {
            float timeToApex = _maxJumpTime / 2;
            _gravity = -2 * _jumpHeight;
            _initialJumpVelocity = 2 * _jumpHeight;
        }

        /// <summary>
        /// Проверка возможности выполнения прыжка и выполнение его
        /// </summary>
        public void HandleJump()
        {
            if (!_isJumping && _playerInput.IsGrounded)
            {
                _player.JumpingAnim(true, true);
                _isJumping = true;
                _currentMovement.y = _initialJumpVelocity;
                _appliedMovement.y = _initialJumpVelocity;
            }
            else if(_isJumping && _playerInput.IsGrounded)
            {
                _isJumping = false;
            }
        }

        /// <summary>
        /// Поворот игрока в направлении движения
        /// </summary>
        public void HandleRotation()
        {
            Vector3 positionToLookAt;
            // The Change in position our character should point to
            positionToLookAt.x = _currentMovement.x;
            positionToLookAt.y = _zero;
            positionToLookAt.z = _currentMovement.z;
            // The current rotation of our character
            // Quaternion currentRotation = transform.rotation;

            if (_playerInput.IsMovementPressed)
            {
                // Create a new rotation based on where the player is currently pressing
                transform.rotation = Quaternion.LookRotation(positionToLookAt);
            }

            if (positionToLookAt.x != 0)
                _rotationVar = positionToLookAt.x;

        }

        /// <summary>
        /// Обработка гравитации, действующей на игрока
        /// </summary>
        public void HandleGravity()
        {
            bool isFalling = _currentMovement.y <= 0.0f; //|| !_playerInput.IsJumpPressed;
            float fallMultiplier = 2.0f;
            // Apply proper gravity depending on if the character is grounded or not
            if (_playerInput.IsGrounded)
            {
                _player.JumpingAnim(false, false);
                _currentMovement.y = _groundedGravity;
                _appliedMovement.y = _groundedGravity;
            }
            else if (isFalling)
            {
                float previousYVelocity = _currentMovement.y;
                _currentMovement.y = _currentMovement.y + (_gravity * fallMultiplier * Time.deltaTime);
                _appliedMovement.y = Mathf.Max((previousYVelocity + _currentMovement.y) * .5f, -20.0f);
            }
            else
            {
                float previousYVelocity = _currentMovement.y;
                _currentMovement.y = _currentMovement.y + (_gravity * Time.deltaTime);
                _appliedMovement.y = (previousYVelocity + _currentMovement.y) * .5f;
            }

            Vector3 _movement = _appliedMovement;
            _movement.x = 0;
            _movement.z = 0;
            _characterController.Move(_movement * Time.deltaTime);

        }

        /// <summary>
        /// Перемещение игрока в заданном направлении с заданной скоростью
        /// </summary>
        public void Move()
        {
            _currentMovement.x = _playerInput.CurrentMovementInput.x * _characterSpeed;
            _currentMovement.z = _playerInput.CurrentMovementInput.y * _characterSpeed;
            _appliedMovement.x = _currentMovement.x;
            _appliedMovement.z = _currentMovement.z;

            _characterController.Move(_appliedMovement * Time.deltaTime);
        }
    }
}
