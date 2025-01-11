namespace Commands.Scripts
{
    public class ClientActionCommand : IActionCommand
    {
        public int GetPlayerId { get; }
        public uint GetActionHexId { get; }
        public uint GetActionOwner { get; }
        public uint GetActionTarget { get; }

        public ClientActionCommand(IActionClientUnit unit, int selectedIndex, uint actionTarget)
        {
            GetPlayerId = unit.GetUnitHostId;
            GetActionOwner = unit.GetUnitHexId;
            GetActionHexId = unit.GetActionHexId(selectedIndex);
            GetActionTarget = actionTarget;
        }
    }
}