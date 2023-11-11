using UnityEngine;

namespace BraidGirl.AI.Movement
{
    public class AIMovementController : BaseMovementController
    {
        public override void Execute(Vector3 destination)
        {
            Vector3 positionWithoutZ = transform.position;

            positionWithoutZ.z = 0;
            destination.z = 0;

            Vector3 moveDestination = destination - positionWithoutZ;
            _movement.Move(moveDestination);
        }
    }
}
