using UnityEngine;

namespace BraidGirl.Scripts.AttractionSystem
{
    /// <summary>
    /// Определяет возможность выполнения притягивания и запускает его
    /// </summary>
    public class AttractController : MonoBehaviour
    {
        private bool _isAttracting;
        private AttractModel _attractModel;
        private bool CanAttract => _attractModel.AttractBoxes.Count != 0;

        /// <summary>
        /// В данный момент выполняется притягивание
        /// </summary>
        public bool IsAttracting => _isAttracting;

        private void Awake()
        {
            _attractModel = GetComponent<AttractModel>();
            _attractModel.Init(ResetAttraction);
        }

        /// <summary>
        /// Перезапуск притягивания
        /// </summary>
        private void ResetAttraction()
        {
            _isAttracting = false;
        }

        /// <summary>
        /// Запуск притягивания
        /// </summary>
        public void TryAttract()
        {
            if (!_isAttracting && CanAttract)
            {
                _isAttracting = true;
                StartCoroutine(_attractModel.Attract());
            }
        }

        /// <summary>
        /// Добавление точек притягивания в соответствующий список
        /// </summary>
        /// <param name="other">Возможная точка притягивания</param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IAttractable attractableBox))
                _attractModel.AttractBoxes.Add(other.gameObject.transform.position);
        }

        /// <summary>
        /// Удаление точек притягивания из списка
        /// </summary>
        /// <param name="other">Возможная точка притягивания</param>
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IAttractable attractableBox))
                _attractModel.AttractBoxes.Remove(other.gameObject.transform.position);
        }
    }
}
