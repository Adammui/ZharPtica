using System;
using System.Collections;
using BraidGirl.Health;
using BraidGirl.Scripts.Health;
using UnityEngine;

namespace BraidGirl.Attack
{
    public class LightAttack : MonoBehaviour, IAttack
    {
        [SerializeField]
        private int _damage;
        [SerializeField]
        private GameObject _weaponCollider;
        [SerializeField]
        private Weapon _weapon;
        [SerializeField]
        private float _duration;

        public GameObject WeaponCollider => _weaponCollider;
        public Weapon Weapon => _weapon;
        public int Damage => _damage;

        private Animator _animator;
        private int _attackHash;
        private WaitForSeconds _waitDuration;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _waitDuration = new WaitForSeconds(_duration);
            _attackHash = Animator.StringToHash("triggerAttack");
            _weapon.Init(HandleAttack);
        }

        private void HandleAttack(GameObject enemy)
        {
            if (enemy != gameObject & enemy.TryGetComponent(out BaseHealthController health))
                health.Damage(_damage, transform.position);
        }

        public IEnumerator Attack()
        {
            _animator.SetTrigger(_attackHash);
            _weaponCollider.SetActive(true);
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
        }
    }
}
