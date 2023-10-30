using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack.Abstract
{
    /// <summary>
    /// Реализует направленную атаку
    /// </summary>
    public interface IDirectionalAttack
    {
        /// <summary>
        /// Выполнение направленной атаки
        /// </summary>
        public void Attack(Vector3 direction);
    }
}
