using BraidGirl.Scripts.AI.Attack;

namespace BraidGirl.AI
{
    public class BossStateManager : IExecute
    {
        private BossAttackController _bossAttackController;

        public BossStateManager(BossAttackController bossAttackController)
        {
            _bossAttackController = bossAttackController;
        }

        public void Execute()
        {
            _bossAttackController.Execute();
        }
    }
}
