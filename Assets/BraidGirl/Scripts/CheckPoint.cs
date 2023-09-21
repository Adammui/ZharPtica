using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BraidGirl
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField]
        private int _checkpointId;
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.name == "Character")
            {
                Player player = GameObject.Find("Player").GetComponent<Player>();
                player.LastCheckpointChange(transform.position, _checkpointId);
            }
        }
    }
}
