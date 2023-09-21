using BraidGirl.Scripts.AI.Patrol;

namespace BraidGirl.AI
{
    public class SwampStateManager : IExecute
    {
        private PatrolController _patrolController;

        public SwampStateManager(PatrolController patrolController)
        {
            _patrolController = patrolController;
        }

        public void Execute()
        {
            _patrolController.Execute();
        }
    }
}
