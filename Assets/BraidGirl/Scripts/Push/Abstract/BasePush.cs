using System;
using System.Collections;
using UnityEngine;

namespace BraidGirl.Scripts.Push.Abstract
{
    public abstract class BasePush : MonoBehaviour
    {
        [SerializeField] protected Vector3 _distance;
        [SerializeField] protected float _speed;
        protected Action _onReset;

        public abstract IEnumerator HandlePush(Vector3 direction);

        public void Init(Action onReset)
        {
            _onReset = onReset;
        }
    }
}
