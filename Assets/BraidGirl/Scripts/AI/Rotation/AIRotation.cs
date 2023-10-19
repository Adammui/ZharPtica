using BraidGirl.AI.Rotation.Abstract;
using UnityEngine;

namespace BraidGirl.AI.Rotation
{
    public class AIRotation : MonoBehaviour, IRotation
    {
        public void Rotate(Vector3 direction)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
