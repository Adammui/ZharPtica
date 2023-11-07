using UnityEngine;

namespace BraidGirl.Abstract
{
    public interface IInitialization
    {
        /// <summary>
        /// Инициализирует класс, используя GameObject
        /// </summary>
        public void Init(GameObject gameObject);
    }
}
