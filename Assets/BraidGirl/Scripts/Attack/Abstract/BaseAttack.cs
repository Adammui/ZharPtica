using System.Collections;
using UnityEngine;

namespace BraidGirl.Attack
{
    /// <summary>
    /// Выполнение и обработка атаки
    /// </summary>
    public abstract class BaseAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _weaponCollider;
        [SerializeField] private Animator _animator;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private int _damage;

        protected GameObject WeaponCollider => _weaponCollider;
        protected Animator Animator => _animator;
        protected int Damage => _damage;

        /// <summary>
        /// Выполнение атаки
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerator Attack();

        /// <summary>
        /// Обработка атаки (выполняется при попадании enemy в область коллайдера Weapon)
        /// </summary>
        /// <param name="enemy">Противник</param>
        protected abstract void HandleAttack(GameObject enemy);

        protected void Start()
        {
            _weapon.Init(HandleAttack);
        }
    }
}
