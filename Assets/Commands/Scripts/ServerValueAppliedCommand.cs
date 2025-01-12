using System.Collections.Generic;

namespace Commands.Scripts
{
    public class ServerValueAppliedCommand : ServerActionCommand
    {
        private bool _allowToZeroHp;
        
        public ServerValueAppliedCommand(IActionCommand copyFromCommand) : base(copyFromCommand)
        {
        }

        public void InsertTemp(List<ServerActionCommand> existedCommands)
        {
            existedCommands.Add(this);
            existedCommands.Sort();
        }
        
        public void ApplyTemp(IBattleTracker battleTracker, ref List<ServerActionCommand> existedCommands)
        {
            existedCommands.Remove(this);
            var casterCondition = battleTracker.GetConditionOfUnit(GetActionOwner);
            if (casterCondition.GetHitPoint.x <= 0)
            {
                // Mark as Dead
            }
            var victimCondition = battleTracker.GetConditionOfUnit(GetActionTarget);
            if (!victimCondition.IsExisted)
            {
                // Mark as Dead
            }
            if (victimCondition.GetHitPoint.x > 0)
            {
                // Do action
            }
            else if (_allowToZeroHp)
            {
                // Do rescue action
            }
        }

        public override IBattleNote ArchiveResult(IBattleTracker<ServerActionCommand> visitor)
        {
            return null;
        }

    }
}