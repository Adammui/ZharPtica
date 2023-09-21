using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BraidGirl
{
    // To turn any object to a passive enemy you should: assign this sctipt to enemy, create collider on it, give object layer 8( same name as class) and tick "IsTrigger" in settings
    public class EnemyFullPassive : MonoBehaviour
    {
        [SerializeField]
        private bool _isAlive = true;
        [SerializeField]
        private int _animationDeathLenght = 3; //TODO: better detect this var automatically
        private int _lastRecievedDamageIdentifier = 0;
        [SerializeField]
        protected bool _isConsumable = true;
        protected Collider _weaponCollider = null;
        //private GameObject Hit; // go for blood paddles generation
        protected UnitHealth enemyHealth;

        // Tach frame checking if character is attacking with weapon detected, also checking for enemy to be alive
        // When all this are true, enemy takes damage and if his hp is 0 it dies dramatically
        protected void Update()
        {
            if (_weaponCollider != null && _isAlive)
            {
                enemyHealth = gameObject.GetComponent<UnitHealth>();
                // enemyHealth.DamageUnit(CharacterAttackController.s_instance.attackDamage);
            }
        }
        // Filtering objects detected by trigger . if the object is on weapon layer, save this game object to weapon coollider object
        public virtual void OnTriggerEnter(Collider collider)
        {

            if (collider.gameObject.layer == 6)
            {
                _weaponCollider = collider;
            }
            Debug.Log("emeny collider tracked obejct: " + collider.gameObject.name);
        }
        // Clearing weapon object if it leaves attack range and no more able to deal damage to this enemy
        public void OnTriggerExit(Collider collider)
        {
            if (_weaponCollider == collider)
            {
                _weaponCollider = null;
            }
        }
        public void Death()
        {
            if (_isConsumable)
            {

                _isAlive = false;
                //TODO: call animator
                //collider.GetComponent<Animator>().SetTrigger("Hit");
                /*Instantiate(Hit, new Vector3(
                   collider.transform.position.x,
                   collider.transform.position.y,
                   collider.transform.position.z),
                   collider.transform.rotation
                   );*/ //blood splashes example
                StartCoroutine(WaitForDeathAnimation());
                Destroy(this.gameObject);
            }
            else
            {
                //TODO: call animator
                //collider.GetComponent<Animator>().SetTrigger("Hit");
                /*Instantiate(Hit, new Vector3(
                   collider.transform.position.x,
                   collider.transform.position.y,
                   collider.transform.position.z),
                   collider.transform.rotation
                   );*/ //blood splashes example
                StartCoroutine(WaitForDeathAnimation());
                enemyHealth.HealUnit(enemyHealth.CurrentMaxHealth);
            }
        }
        public IEnumerator WaitForDeathAnimation()
        {
            yield return new WaitForSeconds(_animationDeathLenght); //it isnt working?
        }
    }
}
