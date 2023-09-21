using System;
using System.Collections;
using System.Collections.Generic;
using BraidGirl.AI.Rest;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack
{
    public class FeatherAttack : MonoBehaviour
    {
        [Header("Projectile params")]

        [SerializeField]
        private GameObject _feather;
        [SerializeField]
        private List<Transform> _featherSpawnPoints;
        [SerializeField]
        private float _delayNextAttack;

        [Space(5)]
        [Header("Rotation params")]

        [SerializeField]
        private Transform _spawnPoints;
        [SerializeField]
        private float _increasedAttackAngle;
        [SerializeField]
        private float _increaseTime;
        [Range(0, 90)]
        [SerializeField]
        private float _stopAttackAngle;

        private Rest _rest;
        private bool _isWaitingRotate;
        private Transform _player;

        private void Awake()
        {
            _rest = GetComponent<Rest>();
        }

        public void Init(Action onReset)
        {
            _rest.Init(onReset);
        }

        public IEnumerator Attack()
        {
            // Vector3 currDirection = direction - transform.position;
            // transform.rotation = Rotator.Rotate(currDirection);

            while (Math.Abs(_spawnPoints.rotation.eulerAngles.x + _stopAttackAngle) % 360 >= 5)
            {
                if (!_isWaitingRotate)
                {
                    _isWaitingRotate = true;
                    StartCoroutine(RotateSpawnPoints());
                }

                foreach (var spawnPoint in _featherSpawnPoints)
                {
                    Instantiate(_feather, spawnPoint.position, spawnPoint.rotation);
                }

                yield return new WaitForSeconds(_delayNextAttack);
            }

            ResetAttack();
        }

        private IEnumerator RotateSpawnPoints()
        {
            yield return new WaitForSeconds(_increaseTime);
            _spawnPoints.Rotate(_increasedAttackAngle, 0, 0);
            _isWaitingRotate = false;
        }

        private void ResetAttack()
        {
            _spawnPoints.localRotation = Quaternion.identity;
            StartCoroutine(_rest.RestHandler());
        }
    }
}
