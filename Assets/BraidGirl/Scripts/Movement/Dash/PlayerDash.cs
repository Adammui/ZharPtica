using System;
using System.Collections;
using BraidGirl.Dash.Abstract;
using UnityEngine;

namespace BraidGirl.Dash
{
    public class PlayerDash : BaseDash
    {
        private float _duration;
        private float _currTime;
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _duration = _distance.x / _speed;
        }

        public override IEnumerator Dash(Vector3 direction)
        {
            _currTime = 0;
            while (_currTime <= _duration)
            {
                _currTime += Time.deltaTime;
                _characterController.Move(direction * (_speed * Time.deltaTime));
                yield return null;
            }
            _onReset.Invoke();
        }

        public override void Init(Action reset)
        {
            _onReset = reset;
        }

    }
}
