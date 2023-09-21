using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BraidGirl
{
    public class UnitHealth : MonoBehaviour
    {
        [SerializeField]
        private int _currentHealth;
        [SerializeField]
        private int _currentMaxHealth;
        [SerializeField]
        private Player _player;

        public void DamageUnit(int damageAmount, Vector3 direction)
        {
            if (_currentHealth > 0)
            {
                _currentHealth -= damageAmount;
            }
            if (_currentHealth <= 0)
            {
                if (gameObject.name == "Character")
                {
                    Player instanceToDie = _player.GetComponent<Player>();
                    instanceToDie.Death();
                }
                else
                {
                    EnemyFullPassive instanceToDie = gameObject.GetComponent<EnemyFullPassive>();
                    instanceToDie.Death();
                }

            }
        }
        public void HealUnit(int healAmount)
        {
            if (_currentHealth < _currentMaxHealth)
            {
                _currentHealth += healAmount;
            }

            if (_currentHealth > _currentMaxHealth)
            {
                _currentHealth = _currentMaxHealth;
            }
        }

        public int CurrentHealth => _currentHealth;
        public int CurrentMaxHealth => _currentMaxHealth;
        public UnitHealth(int currentHealth, int currentMaxHealth, Player player)
        {
            _currentHealth = currentHealth;
            _currentMaxHealth = currentMaxHealth;
            _player = player;
        }
    }
}
