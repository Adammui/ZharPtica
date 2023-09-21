using System;
using System.Collections;
using UnityEngine;

namespace BraidGirl.Scripts.Push.Abstract
{
    public abstract class BasePush : MonoBehaviour
    {
        [SerializeField] protected Vector3 _distance;
        [SerializeField] protected float _speed;

        public abstract IEnumerator HandlePush(Vector3 direction);
    }
}
