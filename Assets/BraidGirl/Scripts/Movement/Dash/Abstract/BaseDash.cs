using System;
using System.Collections;
using UnityEngine;

namespace BraidGirl.Dash.Abstract
{
    /// <summary>
    /// Выполнение логики дэша
    /// </summary>
    public abstract class BaseDash : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        [SerializeField] protected float _cooldown;
        [SerializeField] protected Vector3 _distance;

        protected Action _onReset;

        /// <summary>
        /// Перемещение персонажа в соответствующем направлении direction
        /// </summary>
        /// <param name="direction">Направление движения игрока</param>
        /// <returns></returns>
        public abstract IEnumerator Dash(Vector3 direction);

        /// <summary>
        /// Инициализирует callback, вызываемый при перезапуске дэша
        /// </summary>
        /// <param name="reset">callback, вызываемый при перезапуске дэша</param>
        public abstract void Init(Action reset);
    }
}
