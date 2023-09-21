using UnityEngine;

namespace BraidGirl.Resizing
{
    public static class ResizingProvider
    {
        //TODO: Add change OY for collider
        //TODO: Add handle more colliders

        /// <summary>
        /// Уменьшает размер коллайдера collider в multiplier раз
        /// </summary>
        /// <param name="collider">Уменьшаемый коллайдер</param>
        /// <param name="multiplier">Множитель уменьшения размера</param>
        public static void ReduceHeight(CapsuleCollider collider, float multiplier)
        {
            collider.height /= multiplier; // h: .5;
            Vector3 vector = collider.center; // y: 2;
            vector.y -= collider.height; // h: 2, m: 4, y: 1.5
            collider.center = vector;
        }

        /// <summary>
        /// Увеличивает размер коллайдера collider в multiplier раз
        /// </summary>
        /// <param name="collider">Увеличиваевый коллайдер</param>
        /// <param name="multiplier">Множитель увеличения размера</param>
        public static void IncreaseHeight(CapsuleCollider collider, float multiplier)
        {
            Vector3 vector = collider.center; //y: -.5; y: 1.5
            vector.y += collider.height; //h: .5, m: 4; h: .5, m: 4, y: 2
            collider.center = vector;
            collider.height *= multiplier;
        }
    }
}
