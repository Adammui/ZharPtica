using System;
using System.Collections;
using BraidGirl.Scripts.Push.Abstract;
using UnityEngine;

namespace BraidGirl.Scripts.Push
{
    public class PlayerPush : BasePush
    {
        public override IEnumerator HandlePush(Vector3 direction)
        {
            Vector3 directionDistance = _distance;
            directionDistance.x *= direction.x;
            float _duration = _distance.magnitude / _speed;
            Vector3 startPos = transform.position;
            Vector3 endPos = transform.position + directionDistance;
            float normalizedTime = 0.0f;
            while (normalizedTime < 1.0f)
            {
                float yOffset = _distance.y * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
                transform.position = ((Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up));
                normalizedTime += Time.deltaTime / _duration;
                yield return new WaitForEndOfFrame();
            }
            _onReset.Invoke();
        }
    }
}
