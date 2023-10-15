using System;
using UnityEngine;

namespace BraidGirl.Jump
{
    public class JumpModel : MonoBehaviour
    {
        [Header("Jump")]
        [SerializeField]
        private float _maxJumpHeight = 2.0f;
        [SerializeField]
        private float _maxJumpTime = 0.75f;
        [SerializeField]
        private float _groundedGravity;


        private Action _onGrounded;


        private CharacterController _characterController;
        private Input _inputController;
        private Vector3 _currentMovement;
        private float _initialJumpVelocity;
        private float _gravity;

        public float Gravity => _gravity;
        public Vector3 CurrentMovement => _currentMovement;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();

            SetupJumpVariables();
        }

        private void SetupJumpVariables()
        {
            float timeToApex = _maxJumpTime / 2;
            _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
            _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
        }

        public void Init(Action onGrounded)
        {
            _onGrounded = onGrounded;
        }

        public void Jump()
        {
            _currentMovement = new(0, _initialJumpVelocity, 0);
            _characterController.Move(_currentMovement * Time.deltaTime);
        }

    }
}
