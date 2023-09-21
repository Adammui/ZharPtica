using BraidGirl.Attack;
using BraidGirl.Health;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack
{
    public class GasAttack : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private int _damage;

        private void Awake()
        {
            _weapon.Init(HandleAttack);
        }

        private void HandleAttack(GameObject enemy)
        {
            if (enemy.TryGetComponent(out HealthController health))
            {
                health.Damage(_damage, transform.position);
            }
        }
    }
}
