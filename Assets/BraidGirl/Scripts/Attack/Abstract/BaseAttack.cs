using BraidGirl.Attack;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack.Abstract
{
    public abstract class BaseAttack : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private int _damage;

        protected float Damage => _damage;

        protected abstract void HandleAttack(GameObject enemy);

        protected void Start()
        {
            _weapon.Init(HandleAttack);
        }
    }
}
