using System;
using UnityEngine;

namespace BraidGirl
{
    [CreateAssetMenu(menuName = "ScriptableObject/Attack Anim State Machine", fileName = "Attack Animation State Machine")]
    public class AttackTransitionBehaviour : StateMachineBehaviour
    {
        public static Action s_onResetAttack;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            s_onResetAttack.Invoke();
        }
    }
}
