using System.Collections;
using System.Net;
using BraidGirl.AI.Movement;
using BraidGirl.Attack;
using BraidGirl.Health;
using UnityEngine;

namespace BraidGirl.Projectiles
{
    public class Feather : MonoBehaviour
    {
        [SerializeField]
        private Weapon _weapon;
        [SerializeField]
        private int _damage;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _destructionTime;

        private Rigidbody _rb;

        private void Awake()
        {
            _weapon.Init(HandleAttack);
            // _moving = GetComponent<FeatherMoving>();
            _rb = GetComponent<Rigidbody>();
            StartCoroutine(Destruct());
        }

        private void FixedUpdate()
        {
            _rb.velocity = transform.forward * _speed;
            //_moving.MoveAndRotate(transform.right);
        }

        private void HandleAttack(GameObject enemy)
        {
            if (enemy.TryGetComponent(out HealthController health))
            {
                health.Damage(_damage, transform.position);
            }
            Destroy(gameObject);
        }

        private IEnumerator Destruct()
        {
            yield return new WaitForSeconds(_destructionTime);
            Destroy(gameObject);
        }
    }
}
