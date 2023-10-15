using BraidGirl.Dash.Abstract;
using UnityEngine;

namespace BraidGirl.Dash
{
    /// <summary>
    /// Определяет возможность запуска дэша и запускает его
    /// </summary>
    public class DashController : MonoBehaviour, IExecute
    {
        private BaseDash _dash;
        private bool _canDash = true;
        private bool _isDashing;

        /// <summary>
        /// В данный момент выполняется дэш
        /// </summary>
        public bool IsDashing => _isDashing;

        private void Awake()
        {
            _dash = GetComponent<BaseDash>();
            _dash.Init(ResetDash);
        }

        /// <summary>
        /// Запуск дэша, при выполнении условий запуска
        /// </summary>
        public void Execute()
        {
            if (_canDash && !_isDashing)
            {
                _canDash = false;
                _isDashing = true;
                StartCoroutine(_dash.Dash(transform.forward));
            }
        }

        /// <summary>
        /// Перезапуск дэша
        /// </summary>
        private void ResetDash()
        {
            _canDash = true;
            _isDashing = false;
        }
    }
}
