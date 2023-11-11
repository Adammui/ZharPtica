using System;
using UnityEngine;

namespace BraidGirl.Attack
{
    [Serializable]
    public class Weapon : MonoBehaviour
    {
        private Action<GameObject> _onAttack;

        /// <summary>
        /// Инициализация callback для обработки атаки
        /// </summary>
        /// <param name="handleAttack">callback атаки</param>
        public void Init(Action<GameObject> handleAttack)
        {
            _onAttack = handleAttack;
        }

        /// <summary>
        /// Запуск обработки атаки при входе объекта в область коллайдера
        /// </summary>
        /// <param name="enemy">противник</param>
        private void OnTriggerEnter(Collider enemy)
        {
            _onAttack.Invoke(enemy.gameObject);
        }

        private void OnDestroy()
        {
            _onAttack = null;
        }
    }
}
