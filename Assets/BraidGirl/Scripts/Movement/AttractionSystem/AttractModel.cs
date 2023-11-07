using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BraidGirl.Scripts.AttractionSystem
{
    /// <summary>
    /// Выполнение логики притягивания
    /// </summary>
    public class AttractModel : MonoBehaviour
    {
        [SerializeField]
        private float _attractionSpeed;
        [SerializeField]
        private float _distance;
        [SerializeField]
        private float _delay;

        private WaitForSeconds _waitDelay;
        private List<Vector3> _attractBoxes;
        private Action _onReset;

        public List<Vector3> AttractBoxes => _attractBoxes;

        private void Awake()
        {
            _attractBoxes = new List<Vector3>();
        }

        /// <summary>
        /// Инициализирует callback, выполняемый при окончании притягивания
        /// </summary>
        /// <param name="onReset">callback, вызываемый во время окончания притягивания</param>
        public void Init(Action onReset)
        {
            _onReset = onReset;
        }

        /// <summary>
        /// Выбор ближайщей точки притягивания
        /// </summary>
        /// <returns>Координаты точки притягивания</returns>
        private Vector3 ChooseNearestAttractionBox()
        {
            Vector3 nearest = default;
            float prevDistance;
            float distance = float.MaxValue;

            foreach (var attractBox in _attractBoxes)
            {
                prevDistance = Vector3.Distance(gameObject.transform.position, attractBox);

                if (prevDistance < distance)
                {
                    distance = prevDistance;
                    nearest = attractBox;
                }
            }

            return nearest;
        }

        /// <summary>
        /// Перемещение игрока в точку притягивания
        /// </summary>
        /// <returns></returns>
        public IEnumerator Attract()
        {
            Vector3 nearest = ChooseNearestAttractionBox();
            Vector3 currPosition = transform.position;

#if UNITY_EDITOR
            yield return new WaitForSeconds(_delay);
#else
            yield return _waitDelay;
#endif

            while (true)
            {
                Vector3 targetPosition = new (currPosition.x, nearest.y, currPosition.z);
                transform.position =
                    Vector3.MoveTowards(transform.position, targetPosition, _attractionSpeed * Time.deltaTime);
                if (Math.Abs(transform.position.sqrMagnitude - targetPosition.sqrMagnitude) < _distance)
                {
                    _onReset.Invoke();
                    break;
                }
                yield return null;
            }
        }
    }
}
