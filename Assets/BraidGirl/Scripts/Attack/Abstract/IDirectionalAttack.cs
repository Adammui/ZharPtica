using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack.Abstract
{
    public interface IDirectionalAttack
    {
        public void Attack(Vector3 direction);
    }
}
