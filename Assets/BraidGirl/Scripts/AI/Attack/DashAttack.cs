using System;
using BraidGirl.AI.Rest;
using BraidGirl.Attack;
using BraidGirl.Dash;
using BraidGirl.Dash.Abstract;
using BraidGirl.Health;
using BraidGirl.Rotating;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack
{
    public class DashAttack : MonoBehaviour
    {
        [SerializeField]
        private Collider _weaponCollider;
        [SerializeField]
        private Weapon _weapon;
        [SerializeField]
        private int _damage;
        [SerializeField]
        private Rest _rest;

        private BaseDash _dash;

        private void Awake()
        {
            _dash = GetComponent<EnemyDash>();
            _weapon.Init(HandleAttack);
            _dash.Init(ResetAttack);
        }

        public void Init(Action onResetAttack)
        {
            _rest.Init(onResetAttack);
        }

        private void HandleAttack(GameObject enemy)
        {
            if (enemy.TryGetComponent(out HealthController health))
            {
                health.Damage(_damage, transform.position);
                ((EnemyDash)_dash).StopDash();
            }
        }

        public void Attack(Vector3 direction)
        {
            Vector3 transformWithZ = transform.position;
            transformWithZ.z = 0;
            Vector3 currDirection = direction - transformWithZ;
            transform.rotation = Rotator.Rotate(currDirection);
            _weaponCollider.enabled = true;
            StartCoroutine(_dash.Dash(currDirection.normalized));
        }

        private void ResetAttack()
        {
            _weaponCollider.enabled = false;
            StartCoroutine(_rest.RestHandler());
        }
    }
}
