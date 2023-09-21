using BraidGirl.Attack;
using BraidGirl.Scripts.Health;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack
{
    public class CaveEnterAttack : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private float _damage;

        private void Awake()
        {
            _weapon.Init(HandleAttack);
        }

        private void HandleAttack(GameObject boss)
        {
            if (boss.TryGetComponent(out BossHealthController healthController))
            {
                healthController.Damage(_damage);
            }
        }
    }
}
