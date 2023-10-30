using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack.Abstract
{
    public abstract class BaseProjectileAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _projectile;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private float _spawnDelay;

        protected GameObject Projectile => _projectile;
        protected List<Transform> SpawnPoints => _spawnPoints;
        protected float SpawnDelay => _spawnDelay;

        public abstract void Init(Action onReset);
        protected abstract void ResetAttack();
    }
}
