using System;
using System.Collections;
using UnityEngine;

namespace BraidGirl.Dash.Abstract
{
    public abstract class BaseDash : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        [SerializeField] protected float _cooldown;
        [SerializeField] protected Vector3 _distance;

        protected Action _onReset;

        public abstract IEnumerator Dash(Vector3 direction);
        public abstract void Init(Action reset);
        public abstract void StopDash();
    }
}
