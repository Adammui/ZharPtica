using System;
using System.Collections;
using System.Threading.Tasks;
using BraidGirl.Resizing;
using UnityEngine;

namespace BraidGirl.AI.Rest
{
    public class Rest : MonoBehaviour
    {
        [SerializeField] private float _time;
        [SerializeField] private CapsuleCollider _modelCollider;
        [SerializeField] private float _multiplierChangeSize;

        private Action _onReset;
        public void Init(Action onReset)
        {
            _onReset = onReset;
        }

        public IEnumerator RestHandler()
        {
            ResizingProvider.ReduceHeight(_modelCollider, _multiplierChangeSize);

            yield return new WaitForSeconds(_time);

            ResizingProvider.IncreaseHeight(_modelCollider, _multiplierChangeSize);
            _onReset?.Invoke();
        }
    }
}
