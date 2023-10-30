using System;
using System.Collections;
using BraidGirl.AI.Rest;
using BraidGirl.Scripts.AI.Attack.Abstract;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack
{
    /// <summary>
    /// Атака перьями
    /// </summary>
    public class FeatherAttack : BaseProjectileAttack, IAttack
    {
        [Space(5)]
        [Header("Rotation params")]

        [SerializeField]
        private Transform _spawnPoint;
        [SerializeField]
        private float _increasedAttackAngle;
        [SerializeField]
        private float _increaseTime;
        [Range(0, 90)]
        [SerializeField]
        private float _stopAttackAngle;

        private Rest _rest;
        private bool _isWaitingRotate;

        private void Awake()
        {
            _rest = GetComponent<Rest>();
        }

        public override void Init(Action onReset)
        {
            _rest.Init(onReset);
        }

        public void Attack()
        {
            // Vector3 currDirection = direction - transform.position;
            // transform.rotation = Rotator.Rotate(currDirection);
            StartCoroutine(Activate());
        }

        private IEnumerator Activate()
        {
            while (Math.Abs(_spawnPoint.rotation.eulerAngles.x + _stopAttackAngle) % 360 >= 5)
            {
                if (!_isWaitingRotate)
                {
                    _isWaitingRotate = true;
                    StartCoroutine(RotateSpawnPoints());
                }

                foreach (var spawnPoint in SpawnPoints)
                {
                    Instantiate(Projectile, spawnPoint.position, spawnPoint.rotation);
                }

                yield return new WaitForSeconds(SpawnDelay);
            }

            ResetAttack();
        }

        private IEnumerator RotateSpawnPoints()
        {
            yield return new WaitForSeconds(_increaseTime);
            _spawnPoint.Rotate(_increasedAttackAngle, 0, 0);
            _isWaitingRotate = false;
        }

        protected override void ResetAttack()
        {
            _spawnPoint.localRotation = Quaternion.identity;
            StartCoroutine(_rest.RestHandler());
        }
    }
}
