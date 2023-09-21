using BraidGirl.Dash.Abstract;
using UnityEngine;

namespace BraidGirl.Dash
{
    public class DashController : MonoBehaviour
    {
        private BaseDash _dash;

        private bool _canDash = true;
        private bool _isDashing;

        private void Awake()
        {
            _dash = GetComponent<BaseDash>();
            _dash.Init(ResetDash);
        }

        public void Dash()
        {
            if (_canDash && !_isDashing)
            {
                _canDash = false;
                _isDashing = true;
                StartCoroutine(_dash.Dash(transform.forward));
            }
        }

        private void ResetDash()
        {
            _canDash = true;
            _isDashing = false;
        }
    }
}
