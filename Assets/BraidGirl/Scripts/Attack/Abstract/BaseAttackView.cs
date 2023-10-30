using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack.Abstract
{
    /// <summary>
    /// Содержит и запускает внешние эффекты/анимации
    /// </summary>
    public abstract class BaseAttackView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animationName;

        private int _attackHash;

        protected Animator Animator => _animator;
        protected int AttackHash => _attackHash;

        private void Awake()
        {
            _attackHash = Animator.StringToHash(_animationName);
        }

        /// <summary>
        /// Активация эффектов/анимаций
        /// </summary>
        public abstract void Activate();

        /// <summary>
        /// Деактивация эффектов/анимаций
        /// </summary>
        public abstract void Deactivate();

    }
}
