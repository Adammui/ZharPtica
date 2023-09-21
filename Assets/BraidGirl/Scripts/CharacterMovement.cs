using System.Collections;
using UnityEngine;

namespace BraidGirl
{
    public class CharacterMovement : MonoBehaviour
    {
        // Declare reference variables
        [Header("Movement")]
        [SerializeField]
        private Player _player;
        [SerializeField]
        private float _characterSpeed = 3.0f;
        [SerializeField]
        private float _rotationFactorPerFrame = 1.0f;

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
        private float _maxJumpHeight = 2.0f;
        [SerializeField]
        private float _maxJumpTime = 0.75f;
        private float _initialJumpVelocity;
        private bool _isJumping = false;


        // Dashing variable
        [Header("Dash")]
        [SerializeField]
        public float dashSpeed;
        public float dashingTime;
        public float dashingCooldownTime;
        private bool _isDashing = false;
        private float _lastDashTime;


        // Pushing variable
        [Header("Push")]
        [SerializeField]
        private float _pushSpeed;
        [SerializeField]
        private float _pushingTime;
        private bool _isPushing = false;
        private bool _isPushed = false;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerInput = GetComponent<Input>();
            SetupJumpVariables();
        }

        public void Push()
        {
            _isPushed = true;
        }
        // Calculating Jump variables
        private void SetupJumpVariables()
        {
            float timeToApex = _maxJumpTime / 2;
            _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
            _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
        }
        public void HandleJump()
        {
            if (!_isJumping && _playerInput.IsGrounded && _playerInput.IsJumpPressed)
            {
                _player.JumpingAnim(true, true);
                _isJumping = true;
                _currentMovement.y = _initialJumpVelocity;
                _appliedMovement.y = _initialJumpVelocity;
            }
            else if(!_playerInput.IsJumpPressed && _isJumping && _playerInput.IsGrounded)
            {
                _isJumping = false;
            }
        }

        public void HandleDash()
        {

            if (!_isDashing && _playerInput.IsDashPressed && (_lastDashTime+dashingCooldownTime < Time.time))
            {

                _lastDashTime = Time.time;
                //_player.IsDashing();
                _isDashing = true;
                StartCoroutine(DashCoroutine());

            }
            else if (!_playerInput.IsDashPressed && _isDashing)
            {

                _isDashing = false;
            }
        }

        public void HandlePush()
        {

            if (!_isPushing && _isPushed)
            {
                _player.IsPushing();
                _isPushing = true;
                StartCoroutine(PushCoroutine());
                _isPushed=false;
            }
            else
            {

                _isPushing = false;
            }
        }

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
                // Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
                // transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(positionToLookAt);
            }

            if (positionToLookAt.x != 0)
                _rotationVar = positionToLookAt.x;

        }

        public void HandleGravity()
        {
            bool isFalling = _currentMovement.y <= 0.0f || !_playerInput.IsJumpPressed;
            float fallMultiplier = 2.0f;
            // Apply proper gravity depending on if the character is grounded or not
            if (_characterController.isGrounded)
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
        }

        public void Move()
        {
            _currentMovement.x = _playerInput.CurrentMovementInput.x * _characterSpeed;
            _currentMovement.z = _playerInput.CurrentMovementInput.y * _characterSpeed;
            _appliedMovement.x = _currentMovement.x;
            _appliedMovement.z = _currentMovement.z;

            _characterController.Move(_appliedMovement * Time.deltaTime);
        }
        public void MoveY()
        {
            _currentMovement.x = _playerInput.CurrentMovementInput.x/3 * _characterSpeed;
            _currentMovement.z = _playerInput.CurrentMovementInput.y/3 * _characterSpeed;
            _appliedMovement.x = _currentMovement.x;
            _appliedMovement.z = _currentMovement.z;

            _characterController.Move(_appliedMovement * Time.deltaTime);
        }

        private IEnumerator DashCoroutine()
        {
            float startTime = Time.time; // need to remember this to know how long to dash
            Vector3 directionOfRotation;
            Quaternion rotationForDash;
            if (transform.rotation.y > 0)
            {
                rotationForDash = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
                directionOfRotation = new Vector3(1, 0, 0);
            }
            else
            {
                rotationForDash = Quaternion.Euler(transform.rotation.x, -90, transform.rotation.z);
                directionOfRotation = new Vector3(-1, 0, 0);
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationForDash, Time.deltaTime * 150);

            while (Time.time < startTime + dashingTime)
            {
                _characterController.Move(directionOfRotation * dashSpeed * Time.deltaTime);
                _isDashing = true;
                yield return null; // this will make Unity stop here and continue next frame
            }
        }

        private IEnumerator DashCooldown()
        {
            float startTime = Time.time; // need to remember this to know how long to dash
            while (Time.time < startTime + dashingCooldownTime)
            {
                _characterController.Move(transform.forward * dashSpeed * Time.deltaTime);
                _isDashing = true;
                yield return null; // this will make Unity stop here and continue next frame
            }
        }

        private IEnumerator PushCoroutine()
        {
            float startTime = Time.time;
            while (Time.time < startTime + _pushingTime)
            {
                _characterController.Move(_pushSpeed * Time.deltaTime * (-transform.forward + Vector3.up));
                _isPushing = true;
                yield return null;
            }
        }
    }
}
