using BraidGirl.Health;
using UnityEngine;
using UnityEngine.Events;

namespace BraidGirl.Scripts.AI.Shrub
{
    [RequireComponent(typeof(HealthController))]
    public class Shrub : MonoBehaviour
    {
        private HealthController _healthController;
        private UnityEvent _onDeath;
        private void Start()
        {
            _onDeath = new UnityEvent();
            _onDeath.AddListener(OnDeath);

            _healthController = GetComponent<HealthController>();
            _healthController.Init(_onDeath);
        }

        private void OnDeath()
        {
            gameObject.SetActive(false);
        }
    }
}
