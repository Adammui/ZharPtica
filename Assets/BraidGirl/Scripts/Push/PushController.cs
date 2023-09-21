using BraidGirl.Scripts.Push.Abstract;
using UnityEngine;

namespace BraidGirl.Scripts.Push
{
    public class PushController : MonoBehaviour
    {
        [SerializeField] private BasePush _push;
        private bool _canPush = true;
        public bool IsPushing;
        public void HandlePush(Vector3 enemyPosition)
        {
            if (!_canPush && IsPushing) return;

            Vector3 direction = enemyPosition.x < transform.position.x ?
                Vector3.right : Vector3.left;
            IsPushing = true;
            StartCoroutine(_push.HandlePush(direction));
        }

        public void OnDeath()
        {
            _canPush = false;
        }
    }
}
