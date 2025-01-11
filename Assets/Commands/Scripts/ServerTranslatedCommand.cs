using System;

namespace Commands.Scripts
{
    public class ServerTranslatedCommand : IActionCommand, IComparable<ServerTranslatedCommand>
    {
        public int GetPlayerId { get; }
        public uint GetActionHexId { get; }
        public uint GetActionOwner { get; }
        public uint GetActionTarget { get; }

        public ServerTranslatedCommand(IActionCommand copyFromCommand)
        {
            GetPlayerId = copyFromCommand.GetPlayerId;
            GetActionHexId = copyFromCommand.GetActionHexId;
            GetActionOwner = copyFromCommand.GetActionOwner;
            GetActionTarget = copyFromCommand.GetActionTarget;
        }
        
        private int _calculatedSpeed;
        private int _hiddenSpeed;

        public int CompareTo(ServerTranslatedCommand other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var getSpeedComparison = _calculatedSpeed.CompareTo(other._calculatedSpeed);
            if (getSpeedComparison != 0) return getSpeedComparison;
            return _hiddenSpeed.CompareTo(other._hiddenSpeed);
        }
    }
}