using System;
using BraidGirl.Abstract;
using UnityEngine;

namespace BraidGirl.Gravity
{
    public class GravityController : IExecute, IInitialization
    {
        private GravityModel _gravityModel;

        public void Init(GameObject gameObject)
        {
            _gravityModel = gameObject.GetComponent<GravityModel>();
        }

        public void Init(Action onGrounded)
        {
            _gravityModel.Init(onGrounded);
        }

        public void Execute()
        {
            _gravityModel.Execute();
        }
    }
}
