using BraidGirl.Health;
using BraidGirl.Scripts.AttractionSystem;
using BraidGirl.Scripts.Push;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

namespace BraidGirl
{
    public class Player : MonoBehaviour
    {
        // Input
        [SerializeField]
        private Input _playerInput;

        // Declare reference variables
        [SerializeField]
        private GameObject _character;
        [SerializeField]
        private CharacterMovement _characterMovement;
        [SerializeField]
        private CharacterController _characterController;
        [SerializeField]
        private Vector3 _lastCheckpointPos = new Vector3(0,0,0);
        [SerializeField]
        private int _lastCheckpointId = 0;

        [SerializeField]
        private CharacterAttackController _characterAttackController;
        [SerializeField]
        private AttractController _attractController;
        [SerializeField]
        private GroundChecker _groundChecker;
        [SerializeField]
        private PushController _pushController;
        [SerializeField]
        private HealthController _healthController;

        private UnityEvent _onDeath;
        private UnityEvent<Vector3> _onDamage;
        private Animator _animator;

        // Variable to store optimized setter/getter parameter IDs
        private int _isWalkingHash;
        private int _isJumpingHash;
        private bool _isJumpAnimating = false;


        // death handle
        private WaitForSeconds _waitDeathDuration;
        [SerializeField]
        private float _deathAnimDuration;


        private void Awake()
        {
            _onDeath = new UnityEvent();
            _onDamage = new UnityEvent<Vector3>();
            _onDeath.AddListener(Death);
            _onDamage.AddListener(_pushController.HandlePush);

            _animator = _character.GetComponent<Animator>();
            _isWalkingHash = Animator.StringToHash("isWalking");
            _isJumpingHash = Animator.StringToHash("isJumping");
            _healthController.Init(_onDeath, _onDamage);
            //gameObject.transform.position = _lastCheckpointPos;
        }

        private void HandleAnimation()
        {
            // Get parameters values from animator
            bool isWalking = _animator.GetBool(_isWalkingHash);

            // Start Walking if _isMovementPressed is true and already walking
            if (_playerInput.IsMovementPressed && !isWalking)
            {
                _animator.SetBool(_isWalkingHash, true);
            }
            // Stop Walking if _isMovementPressed is false and not already walking
            else if (!_playerInput.IsMovementPressed && isWalking)
            {
                _animator.SetBool(_isWalkingHash, false);
            }
        }
        public void JumpingAnim(bool _isJumpingH, bool _isJumpAnim)
        {
            _animator.SetBool(_isJumpingHash, _isJumpingH);
            _isJumpAnimating = _isJumpAnim;
        }
        public void LastCheckpointChange(Vector3 newCheckpointPos, int lastCheckpointId)
        {
            _lastCheckpointPos = newCheckpointPos;
            _lastCheckpointId = lastCheckpointId;
        }
        public Vector3 GetLastCheckPoint()
        {
            return _lastCheckpointPos;
        }
        public int GetLastCheckPointId()
        {
            return _lastCheckpointId;
        }
        public void Death()
        {
            Debug.Log("Player died");
            _characterController = _character.GetComponent<CharacterController>();
            _waitDeathDuration = new WaitForSeconds(_deathAnimDuration);
            StartCoroutine(HandleDeath());

        }
        public IEnumerator HandleDeath()
        {
            _animator.Play("Death");
            yield return _waitDeathDuration;
            HandleRevive();
        }
        public void HandleRevive()
        {
            Debug.Log("Player revived");
            _characterController.Move(GetLastCheckPoint() - _character.transform.position);
            _animator.Play("Stand_up");
            _healthController.Revive();
        }

        public void IsDashing(/*bool _isJumpingH, bool _isJumpAnim*/)
        {
            /*_animator.SetBool(_isJumpingHash, _isJumpingH);
            _isJumpAnimating = _isJumpAnim;*/
        }
        public void IsPushing(/*bool _isJumpingH, bool _isJumpAnim*/)
        {
            /*_animator.SetBool(_isJumpingHash, _isJumpingH);
            _isJumpAnimating = _isJumpAnim;*/
        }

        private void FixedUpdate()
        {
            _playerInput.IsGrounded = _groundChecker.Execute();
        }

        private void Update()
        {
            _characterMovement.HandleRotation();
            HandleAnimation();
            if (!_pushController.IsPushing)
            {
                if ((_playerInput.IsAttractPressed && _attractController.CanAttract) || _attractController.IsAttracting)
                {
                    _attractController.TryAttract();
                }
                else
                {
                    if (_playerInput.IsLightAttack && !_characterAttackController.isAttacking)
                        _characterAttackController.HandleLightAttack();
                    else if (_playerInput.IsHeavyAttack && !_characterAttackController.isAttacking)
                        _characterAttackController.HandleHeavyAttack();
                    else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) { }
                    else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Stand_up")) { }
                    else if (!_animator.GetCurrentAnimatorStateInfo(1).IsName("Idle")) // remove to make character move while attacking
                        _characterMovement.MoveY();
                    else if (_animator.GetCurrentAnimatorStateInfo(1).IsName("Idle"))
                        _characterMovement.Move();

                }

                _characterMovement.HandleGravity();
                _characterMovement.HandleJump();
                _characterMovement.HandleDash();
            }
        }
    }

}
