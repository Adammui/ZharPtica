using UnityEngine;

namespace BraidGirl.Jump
{
    public class JumpView : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animationName;

        private int _animationHash;

        private void Awake()
        {
            _animationHash = Animator.StringToHash(_animationName);
        }

        public void Activate()
        {
            _animator.SetBool(_animationHash, true);
        }

        public void Deactivate()
        {
            _animator.SetBool(_animationHash, false);
        }
    }
}
