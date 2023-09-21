using System.Collections.Generic;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Patrol
{
    public class Patrol : MonoBehaviour
    {
        [SerializeField] private List<Transform> _points;
        public List<Transform> Points => _points;
        private int _index;

        public Vector3 GetNextPoint()
        {
            Vector3 position = _points[_index].position;
            AddIndex();
            return position;
        }

        private void AddIndex()
        {
            _index++;
            if (_index >= _points.Count)
                _index = 0;
        }
    }
}
