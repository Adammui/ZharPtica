using System;
using UnityEngine;

namespace BraidGirl.Health
{
    [Serializable]
    public class Health
    {
        [SerializeField]
        private float _maxHealth;
        private float _health;

        public void Init() => _health = _maxHealth;

        public float Damage(float value) => _health = Mathf.Clamp(_health - value, 0, _maxHealth);
        public void Revive() => _health = _maxHealth;

        public void Kill() => _health = 0;

        public bool IsHealthLess(float percent)
        {
            return _health <= _maxHealth * percent;
        }
    }
}
