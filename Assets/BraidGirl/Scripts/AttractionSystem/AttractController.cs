using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BraidGirl.Scripts.AttractionSystem
{
    public class AttractController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _character;
        [SerializeField]
        private float _attractionSpeed;
        [SerializeField]
        private float _distance;
        [SerializeField]
        private float _delay;

        private Animator _animator;

        private WaitForSeconds _waitDelay;
        private List<Vector3> _attractBoxes;
        private bool _isAttracting;

        public bool IsAttracting => _isAttracting;
        public bool CanAttract => _attractBoxes.Count != 0;

        private void Awake()
        {
            _attractBoxes = new List<Vector3>();
            _animator = _character.GetComponent<Animator>();
        }

        public void TryAttract()
        {
            if (!_isAttracting)
            {
                _isAttracting = true;
                _animator.Play("attraction");
                Vector3 nearest = ChooseNearestAttractionBox();
                StartCoroutine(Attract(nearest));
            }
        }

        private Vector3 ChooseNearestAttractionBox()
        {
            Vector3 nearest = default;
            float prevDistance;
            float distance = float.MaxValue;

            for (int i = 0; i < _attractBoxes.Count; i++)
            {
                prevDistance = Vector3.Distance(gameObject.transform.position, _attractBoxes[i]);

                if (prevDistance < distance)
                {
                    distance = prevDistance;
                    nearest = _attractBoxes[i];
                }
            }

            return nearest;
        }

        private IEnumerator Attract(Vector3 _nearest)
        {
            #if UNITY_EDITOR
                yield return new WaitForSeconds(_delay);
            #else
                yield return _waitDelay;
            #endif

            Vector3 currPosition = transform.position;
            while (_isAttracting)
            {
                Vector3 targetPosition = new (currPosition.x, _nearest.y, currPosition.z);
                transform.position =
                    Vector3.MoveTowards(transform.position, targetPosition, _attractionSpeed * Time.deltaTime);
                if (Math.Abs(transform.position.sqrMagnitude - targetPosition.sqrMagnitude) < _distance)
                    ResetAttraction();
                yield return new WaitForEndOfFrame();
            }
        }

        private void ResetAttraction()
        {
            _isAttracting = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IAttractable attractableBox))
                _attractBoxes.Add(other.gameObject.transform.position);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IAttractable attractableBox))
                _attractBoxes.Remove(other.gameObject.transform.position);
        }
    }
}
