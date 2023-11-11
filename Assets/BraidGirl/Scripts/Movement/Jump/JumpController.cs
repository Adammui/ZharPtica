using BraidGirl.Abstract;
using BraidGirl.Gravity;
using UnityEngine;

namespace BraidGirl.Jump
{
    public class JumpController : IInitialization
    {
        private GravityController _gravityController;
        private JumpModel _jumpModel;
        private JumpView _jumpView;
        private bool _isJumping;
        private bool _canJump;

        public bool IsJumping => _isJumping;

        public JumpController(GravityController gravityController)
        {
            _gravityController = gravityController;
        }

        public void Init(GameObject gameObject)
        {
            _jumpModel = gameObject.GetComponent<JumpModel>();
            _jumpView = gameObject.GetComponent<JumpView>();
            _gravityController.Init(ResetJump);
        }

        public void Execute()
        {
            if (!_isJumping)
            {
                _isJumping = true;
                _jumpView.Activate();
                _jumpModel.Jump();
            }

           
        }

        private void ResetJump()
        {
            _isJumping = false;
            _jumpView.Deactivate();
        }
    }
}
