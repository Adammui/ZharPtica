using System;
using UnityEngine;

namespace BraidGirl.AI.Movement
{
    public abstract class BaseMovementController : MonoBehaviour
    {
        protected IMovement _movement;

        private void Awake()
        {
            _movement = GetComponent<IMovement>();
        }

        public abstract void Execute(Vector3 destination);
    }
}
