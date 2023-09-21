using System;
using System.Collections.Generic;
using UnityEngine;

namespace BraidGirl.AI
{
    public class PlayerFinder : MonoBehaviour
    {
        [Serializable]
        private class Area
        {
            public Vector3 areaSize;
            public FinderType findAreaType;
            public Vector3 offset;
        }

        [SerializeField]
        private LayerMask _playerLayer;
        [SerializeField]
        private List<Area> _areaList;

        private Collider[] _colliders;
        private Collider _playerPosition;

        public Collider PlayerPosition => _playerPosition;

        //TODO: Добавить поворот в нужную позицию, вместо свапа между сторонами
        public void Rotate()
        {
            foreach (var area in _areaList)
                area.offset = -area.offset;
        }

        /// <summary>
        /// Проверяет наличие игрока в области
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Результат поиска</returns>
        public bool IsPlayerInArea(FinderType type)
        {
            foreach (var area in _areaList)
            {
                // if (_playerPosition == null)
                _playerPosition = KeepPlayerInArea(area);

                if (area.findAreaType == type)
                    return Physics.CheckBox(transform.position + area.offset, area.areaSize / 2,
                        Quaternion.identity, _playerLayer);
            }
            return false;
        }

        private Collider KeepPlayerInArea(Area area)
        {
            _colliders = Physics.OverlapBox(transform.position + area.offset, area.areaSize,
                Quaternion.identity, _playerLayer);
            return _colliders.Length > 0 ? _colliders[0] : null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            foreach (var area in _areaList)
            {
                Gizmos.DrawWireCube(transform.position + area.offset, area.areaSize);
                Gizmos.color = Color.red;
            }
        }
    }
}
