using UnityEngine;

namespace BraidGirl.AI.Movement
{
    public interface IMovement
    {
        protected float Speed { get; }

        public void Move(Vector3 destination);

        public void OnDeath();
    }
}
