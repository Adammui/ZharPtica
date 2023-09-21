using UnityEngine;

namespace BraidGirl.Rotating
{
    public class Rotator
    {
        public static Quaternion Rotate(Vector3 destination)
        {
            Vector3 lookPos = new (destination.x, 0, destination.z);
            return Quaternion.LookRotation(lookPos);
        }
    }
}
