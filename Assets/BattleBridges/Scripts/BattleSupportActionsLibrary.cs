using BattleBridges.Scripts.Commands;
using BattleBridges.Scripts.Managers;
using Fusion;

namespace BattleBridges.Scripts
{
    public static class BattleSupportActionsLibrary
    {
        public static void DeliveryActionCommand(PlayerRef playerRef, int selectedIndex)
        {
            var localUnit = SoBattleTrackerCentreBridge.GetLocalUnit(playerRef, selectedIndex);
            AttributeValueAppliedCommand command = new AttributeValueAppliedCommand(localUnit, selectedIndex, (1, 0));
            SoBattleTrackerCentreBridge.OnServerCommandReceived(command);
        }
    }
}