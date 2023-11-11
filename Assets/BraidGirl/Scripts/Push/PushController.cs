using BraidGirl.Scripts.Push.Abstract;
using UnityEngine;

namespace BraidGirl.Scripts.Push
{
    public class PushController : MonoBehaviour
    {
        [SerializeField] private BasePush _push;
        private bool _canPush = true;
        private bool _isPushing;
        public bool IsPushing => _isPushing;

        private void Awake()
        {
            _push.Init(OnReset);
        }

        public void HandlePush(Vector3 enemyPosition)
        {
            if (!_canPush && IsPushing) return;

            Vector3 direction = enemyPosition.x < transform.position.x ?
                Vector3.right : Vector3.left;
            _isPushing = true;
            StartCoroutine(_push.HandlePush(direction));
        }

        private void OnReset()
        {
            _isPushing = false;
        }

        public void OnDeath()
        {
            _canPush = false;
        }
    }
}
