using System;
using System.Collections;
using UnityEngine;

namespace BraidGirl.Attack
{
    public interface IAttack
    {
        public GameObject WeaponCollider { get; }
        public Weapon Weapon { get; }
        public int Damage { get; }
        public IEnumerator Attack();
        public void ResetAttack();
    }
}
