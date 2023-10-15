using BraidGirl.Dash;
using BraidGirl.Gravity;
using BraidGirl.Health;
using BraidGirl.Jump;
using BraidGirl.Scripts.Attack.Player;
using BraidGirl.Scripts.AttractionSystem;
using BraidGirl.Scripts.Push;
using UnityEngine;
using UnityEngine.Events;

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
        private AttractController _attractController;
        [SerializeField]
        private GroundChecker _groundChecker;
        [SerializeField]
        private PushController _pushController;
        [SerializeField]
        private HealthController _healthController;
        [SerializeField]
        private DashController _dashController;

        private PlayerAttackController _playerAttackController;
        private JumpController _jumpController;
        private GravityController _gravityController;


        private UnityEvent _onDeath;
        private UnityEvent<Vector3> _onDamage;
        private Animator _animator;

        // Variable to store optimized setter/getter parameter IDs
        private int _isWalkingHash;
        private int _isJumpingHash;
        private bool _isJumpAnimating = false;

        private void Awake()
        {
            _playerAttackController = new ();
            _gravityController = new GravityController();
            _jumpController = new JumpController(_gravityController);

            _onDeath = new UnityEvent();
            _onDamage = new UnityEvent<Vector3>();
            _onDeath.AddListener(Death);
            _onDamage.AddListener(_pushController.HandlePush);

            _playerAttackController.Init(transform.GetChild(0).gameObject);
            _gravityController.Init(transform.GetChild(0).gameObject);
            _jumpController.Init(transform.GetChild(0).gameObject);

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
        private void Death()
        {
            _characterController = _character.GetComponent<CharacterController>();
            _characterController.Move(GetLastCheckPoint() - _character.transform.position);
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
            if (_pushController.IsPushing) return;

            if (_attractController.IsAttracting || _playerInput.IsAttractPressed)
            {
                _attractController.TryAttract();
            }
            else if (_dashController.IsDashing || _playerInput.IsDashPressed)
            {
                _dashController.Execute();
            }
            else if ((_playerAttackController.IsAttacking && !_playerInput.IsLightAttack) || _playerInput.IsHeavyAttack)
            {
                _playerAttackController.TryAttack();
                _characterMovement.HandleGravity();
            }
            else
            {
                _characterMovement.HandleRotation();
                HandleAnimation();

                if(_playerInput.IsLightAttack)
                    _playerAttackController.TryAttack();

                _characterMovement.HandleGravity();
                _characterMovement.HandleJump();
                // if(_playerInput.IsJumpPressed || _jumpController.IsJumping)
                //     _jumpController.Execute();
                _characterMovement.Move();
            }
        }
    }

}
