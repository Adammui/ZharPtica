using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BraidGirl
{
    public class EnemyPassiveDamage : EnemyFullPassive
    {
        [SerializeField]
        private int _basicDamage = 1;
        [SerializeField]
        private CharacterController _characterController;
        [SerializeField]
        private Player _player;
        public override void OnTriggerEnter(Collider collider)
        {
            base.OnTriggerEnter(collider);
            if (collider.name == "Character")
            {
                CharacterMovement _characterMovement = collider.GetComponent<CharacterMovement>();
                UnitHealth playerHealth = collider.GetComponent<UnitHealth>();
                if (playerHealth.CurrentHealth > _basicDamage)
                {
                    _characterMovement.Push();
                }
                playerHealth.DamageUnit(_basicDamage, transform.position);


            }
        }

    }
}
