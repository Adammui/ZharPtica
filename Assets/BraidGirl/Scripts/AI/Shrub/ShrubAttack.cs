using BraidGirl.Attack;
using BraidGirl.Health;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Shrub
{
    [RequireComponent(typeof(Weapon))]
    public class ShrubAttack : MonoBehaviour
    {
        [SerializeField] private int _damage;
        private Weapon _weapon;

        private void Start()
        {
            _weapon = GetComponent<Weapon>();
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
