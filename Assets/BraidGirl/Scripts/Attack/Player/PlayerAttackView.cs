using UnityEngine;

namespace BraidGirl.Scripts.Attack.Player
{
    public class PlayerAttackView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string[] _animationsTransitions;
        private int[] _animationsHash;

        private void Awake()
        {
            _animationsHash = new int[_animationsTransitions.Length];

            for (int i = 0; i < _animationsTransitions.Length; i++)
            {
                _animationsHash[i] = Animator.StringToHash(_animationsTransitions[i]);
            }
        }

        public void Activate(int attackType)
        {
            _animator.SetTrigger(_animationsHash[attackType]);
        }
    }
}
