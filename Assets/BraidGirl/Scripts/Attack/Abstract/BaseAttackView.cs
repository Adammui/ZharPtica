using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack.Abstract
{
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

        public abstract void Activate();

        public abstract void Deactivate();

    }
}
