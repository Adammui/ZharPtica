using System;
using Unity.VisualScripting;
using UnityEngine;

namespace BraidGirl.Attack
{
    [Serializable]
    public class Weapon : MonoBehaviour
    {
        private Action<GameObject> _onAttack;

        public void Init(Action<GameObject> handleAttack)
        {
            _onAttack = handleAttack;
        }

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
