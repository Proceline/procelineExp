using BattleBridges.Scripts.Controller;
using BattleBridges.Scripts.Managers;
using Commands.Scripts;
using UnityEngine;

namespace BattleBridges.Scripts.Commands
{
    public class AttributeValueAppliedCommand : ServerActionCommand
    {
        private bool _allowToZeroHp;
        
        public AttributeValueAppliedCommand(IActionClientUnit unit, int selectedIndex, (int, int) actionTarget) : base(unit, selectedIndex, actionTarget)
        {
        }

        public override void ArchiveResult(SoBattleTrackerCentreBridge visitor)
        {
            var castUnit = visitor.GetConditionOfUnit(GetActionOwner);
            if (castUnit.GetHitPoint.x <= 0)
            {
                // Mark as Dead
                return;
            }
            var targetUnit = visitor.GetConditionOfUnit(GetActionTarget);
            if (!targetUnit.IsExisted)
            {
                // Mark as Dead
            }

            Vector2Int hostPair = new Vector2Int(castUnit.GetUnitIdentity.Item1, castUnit.GetUnitIdentity.Item2);
            Vector2Int targetPair = new Vector2Int(targetUnit.GetUnitIdentity.Item1, targetUnit.GetUnitIdentity.Item2);
            if (targetUnit.GetHitPoint.x > 0)
            {
                var damage = 10f;
                var resultValue = 90;
                targetUnit.ReceiveDamage(damage, resultValue);
                ServerNotesAnalyzer.EnqueueServerMessage(0, orderIndex =>
                {
                    SoBattleTrackerCentreBridge.GetExistedAnalyzer.RpcSendAnimationNote(orderIndex, 0, hostPair,
                        0x0000);
                });
                ServerNotesAnalyzer.EnqueueServerMessage(1, orderIndex =>
                {
                    SoBattleTrackerCentreBridge.GetExistedAnalyzer.RpcSendAnimationNote(orderIndex, 5, targetPair,
                        0x0001);
                });
                ServerNotesAnalyzer.EnqueueServerMessage(2, orderIndex =>
                {
                    SoBattleTrackerCentreBridge.GetExistedAnalyzer.RpcSendActionNote(orderIndex, 5, hostPair, targetPair,
                        new Vector3Int(100, resultValue, Mathf.RoundToInt(damage)));
                });
            }
            else if (_allowToZeroHp)
            {
                // Do rescue action
            }
        }
    }
}