using System.Collections.Generic;
using BraidGirl.Abstract;
using UnityEngine;

namespace BraidGirl.Scripts.AI.Attack.Abstract
{
    public abstract class BaseActiveAttackController : IInitialization, IExecute
    {
        protected List<BaseAttack> _baseAttacks = new List<BaseAttack>();
        protected bool _isAttacking;

        public bool IsAttacking => _isAttacking;

        public abstract void Init(GameObject gameObject);

        public abstract void Execute();

        protected abstract void ResetAttack();
    }
}
