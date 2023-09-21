using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BraidGirl
{
    public class HealSpot : MonoBehaviour
    {
        [SerializeField]
        private int _basicHeal;
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.name == "Character")
            {
                UnitHealth unitHealth = collision.GetComponent<UnitHealth>();
                if (unitHealth != null)
                {
                    unitHealth.HealUnit(_basicHeal);
                }
            }
        }
    }
}
