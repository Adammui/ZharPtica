using System;
using BraidGirl.Jump;
using UnityEngine;

namespace BraidGirl.Gravity
{
    public class GravityModel : MonoBehaviour
    {
        [SerializeField]
        private float _groundedGravity;

        private Input _inputController;
        private CharacterController _characterController;
        private JumpModel _jumpModel;

        private Action _onGrounded;
        private float _gravity;

        private void Awake()
        {
            _inputController = GetComponent<Input>();
            _characterController = GetComponent<CharacterController>();
            _jumpModel = GetComponent<JumpModel>();
        }

        private void Start()
        {
            _gravity = _jumpModel.Gravity;
        }

        public void Init(Action onGrounded)
        {
            _onGrounded = onGrounded;
        }

        public void Execute()
        {
            Vector3 _currentMovement = _jumpModel.CurrentMovement;
            bool isFalling = _currentMovement.y <= 0.0f || !_inputController.IsJumpPressed;
            float fallMultiplier = 2.0f;
            // Apply proper gravity depending on if the character is grounded or not
            if (_inputController.IsGrounded)
            {
                _onGrounded?.Invoke();
                _currentMovement.y = _groundedGravity;
                //_appliedMovement.y = _groundedGravity;
            }
            else if (isFalling)
            {
                float previousYVelocity = _currentMovement.y;
                _currentMovement.y = _currentMovement.y + (_gravity * fallMultiplier * Time.deltaTime);
                _currentMovement.y = Mathf.Max((previousYVelocity + _currentMovement.y) * .5f, -20.0f);
            }
            else
            {
                float previousYVelocity = _currentMovement.y;
                _currentMovement.y = _currentMovement.y + (_gravity * Time.deltaTime);
                _currentMovement.y = (previousYVelocity + _currentMovement.y) * .5f;
            }
            _characterController.Move(_currentMovement * Time.deltaTime);
        }
    }
}
