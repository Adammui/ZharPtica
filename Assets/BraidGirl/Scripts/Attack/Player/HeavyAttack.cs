using System;
using System.Collections;
using BraidGirl.Health;
using UnityEngine;

namespace BraidGirl.Attack
{
    [RequireComponent(typeof(CharacterMovement))]
    public class HeavyAttack : MonoBehaviour, IAttack
    {
        [SerializeField]
        private GameObject _weaponCollider;
        [SerializeField]
        private GameObject _airWeaponCollider;

        [SerializeField]
        private Weapon _weapon;
        [SerializeField]
        private Weapon _airWeapon;

        [SerializeField]
        private int _damage;
        [SerializeField]
        private int _airDamage;

        [SerializeField]
        private float _airAttackGravity;
        [SerializeField]
        private float _duration;

        public GameObject WeaponCollider => _weaponCollider;
        public Weapon Weapon => _weapon;
        public int Damage => _damage;

        //TODO: Remove input from here
        private Input _input;
        private CharacterMovement _characterMovement;
        private Action _onResetAttack;
        private Animator _animator;
        private float _defaultGravity;
        private int _attackHash;
        private WaitForSeconds _waitDuration;

        private void Start()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _animator = GetComponent<Animator>();
            _input = GetComponent<Input>();
            _waitDuration = new WaitForSeconds(_duration);
            _attackHash = Animator.StringToHash("triggerRangedAttack");

            _defaultGravity = _characterMovement.Gravity;
            _weapon.Init(HandleAttack);
            _airWeapon.Init(HandleAttack);
        }

        private void HandleAttack(GameObject enemy)
        {
            if (enemy != gameObject & enemy.TryGetComponent(out BaseHealthController health))
            {
                if (_input.IsGrounded)
                    health.Damage(_damage, transform.position);
                else
                    health.Damage(_airDamage, transform.position);
            }
        }

        public IEnumerator Attack()
        {
            if (_input.IsGrounded)
            {
                _animator.SetTrigger(_attackHash);
                _weaponCollider.SetActive(true);
            }
            else
            {
                _characterMovement.Gravity = _airAttackGravity;
                _animator.SetTrigger(_attackHash);
                _airWeaponCollider.SetActive(true);
            }
            #if UNITY_EDITOR
                yield return new WaitForSeconds(_duration);
            #else
                yield return _waitDuration;
            #endif
            ResetAttack();
        }

        public void ResetAttack()
        {
            _weaponCollider.SetActive(false);
            _airWeaponCollider.SetActive(false);
            _characterMovement.Gravity = _defaultGravity;
        }
    }
}
