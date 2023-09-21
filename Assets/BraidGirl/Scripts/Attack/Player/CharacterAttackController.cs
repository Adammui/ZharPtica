using BraidGirl.Attack;
using UnityEngine;

namespace BraidGirl
{
    public class CharacterAttackController : MonoBehaviour
    {
        public bool canAttack = true;
        public bool isAttacking;

        private IAttack _lightAttack;
        private IAttack _heavyAttack;

        private void Awake()
        {
            _lightAttack = GetComponent<LightAttack>();
            _heavyAttack = GetComponent<HeavyAttack>();

            AttackTransitionBehaviour.s_onResetAttack += SetCanAttack;
            //  AttackTransitionBehaviour.s_onResetAttack += _heavyAttack.ResetAttack;
        }

        public void HandleLightAttack()
        {
            if (canAttack && !isAttacking)
            {
                canAttack = false;
                isAttacking = true;
                StartCoroutine(_lightAttack.Attack());
            }
        }

        public void HandleHeavyAttack()
        {
            if (canAttack && !isAttacking)
            {
                canAttack = false;
                isAttacking = true;
                StartCoroutine(_heavyAttack.Attack());
            }
        }

        private void SetCanAttack()
        {
            canAttack = true;
            isAttacking = false;
        }
    }

}
