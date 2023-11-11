using System;
using System.Collections;
using BraidGirl.Scripts.Push.Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace BraidGirl.Scripts.Push
{
    public class EnemyPush : BasePush
    {
        private NavMeshAgent _agent;
        // _distance = _speed * _time
        // _time = _distance / _speed
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public override IEnumerator HandlePush(Vector3 direction)
        {
            Vector3 directionDistance = _distance;
            directionDistance.x *= direction.x;
            float _duration = _distance.magnitude / _speed;
            Vector3 startPos = _agent.transform.position;
            Vector3 endPos = _agent.transform.position + directionDistance; //* _agent.baseOffset;
            float normalizedTime = 0.0f;
            while (normalizedTime < 1.0f)
            {
                float yOffset = _distance.y * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
                _agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
                normalizedTime += Time.deltaTime / _duration;
                yield return null;
            }
            _onReset.Invoke();
        }
    }
}
