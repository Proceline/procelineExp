using System;
using BattleBridges.Scripts.Managers;
using Commands.Scripts;

namespace BattleBridges.Scripts.Commands
{
    public abstract class ServerActionCommand : IActionCommand<SoBattleTrackerCentreBridge>, IComparable<ServerActionCommand>
    {
        public abstract void ArchiveResult(SoBattleTrackerCentreBridge visitor);

        public uint GetActionHexId { get; }
        public (int, int) GetActionOwner { get; }
        public (int, int) GetActionTarget { get; }

        protected ServerActionCommand(IActionClientUnit unit, int selectedIndex, (int, int) actionTarget)
        {
            GetActionOwner = (unit.GetUnitHostId, unit.GetUnitIndex);
            GetActionHexId = unit.GetActionHexId(selectedIndex);
            GetActionTarget = actionTarget;
            _calculatedSpeed = 10 - unit.GetUnitHostId;
            _hiddenSpeed = 0;
        }

        private readonly int _calculatedSpeed;
        private readonly int _hiddenSpeed;

        public int CompareTo(ServerActionCommand other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var getSpeedComparison = _calculatedSpeed.CompareTo(other._calculatedSpeed);
            return getSpeedComparison != 0 ? getSpeedComparison : _hiddenSpeed.CompareTo(other._hiddenSpeed);
        }
    }
}