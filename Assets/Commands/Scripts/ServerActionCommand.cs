using System;

namespace Commands.Scripts
{
    public abstract class ServerActionCommand : IActionCommand<IBattleNote, IBattleTracker<ServerActionCommand>>,
        IComparable<ServerActionCommand>
    {
        public abstract IBattleNote ArchiveResult(IBattleTracker<ServerActionCommand> visitor);

        public int GetPlayerId { get; }
        public uint GetActionHexId { get; }
        public uint GetActionOwner { get; }
        public uint GetActionTarget { get; }

        protected ServerActionCommand(IActionCommand copyFromCommand)
        {
            GetPlayerId = copyFromCommand.GetPlayerId;
            GetActionHexId = copyFromCommand.GetActionHexId;
            GetActionOwner = copyFromCommand.GetActionOwner;
            GetActionTarget = copyFromCommand.GetActionTarget;
        }

        private int _calculatedSpeed;
        private int _hiddenSpeed;

        public int CompareTo(ServerActionCommand other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var getSpeedComparison = _calculatedSpeed.CompareTo(other._calculatedSpeed);
            return getSpeedComparison != 0 ? getSpeedComparison : _hiddenSpeed.CompareTo(other._hiddenSpeed);
        }
    }
}