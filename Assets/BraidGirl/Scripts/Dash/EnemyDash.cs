using System;
using System.Collections;
using BraidGirl.Dash.Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace BraidGirl.Dash
{
    public class EnemyDash : BaseDash
    {
        private float _currTime;
        private NavMeshAgent _agent;
        private float _duration;
        private bool _dashStopped;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _duration = _distance.x / _speed;
        }

        public override IEnumerator Dash(Vector3 direction)
        {
            _currTime = 0;
            while (_currTime <= _duration)
            {
                _currTime += Time.deltaTime;
                _agent.velocity = direction * _speed;
                yield return null;
            }

            _agent.velocity = Vector3.zero;
            _onReset.Invoke();
        }

        public override void StopDash()
        {
            _currTime = _duration;
            _dashStopped = true;
        }

        public override void Init(Action reset)
        {
            _onReset = reset;
        }
    }
}
