using BattleBridges.Scripts.BattleNotes;
using BattleBridges.Scripts.Managers;
using Commands.Scripts;
using Network.Scripts;
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
                var animationNote = new BattleNoteAnimationInput
                {
                    AnimatingCharacter = hostPair,
                    AnimationHexId = 0x0000,
                    Timeline = 0
                };
                var onHitNote = new BattleNoteAnimationInput
                {
                    AnimatingCharacter = targetPair,
                    AnimationHexId = 0x0001,
                    Timeline = 5
                };
                var battleNote = new BattleNoteAttributeValueInput
                {
                    AfterValue = resultValue,
                    AttributeFieldId = 0x0000, // HitPoint
                    BeforeValue = 100,
                    DeltaValue = Mathf.CeilToInt(damage),
                    FromUnit = hostPair,
                    TargetUnit = targetPair,
                    Timeline = 5,
                };
                ServerEssentialSpawner.EnqueueServerMessage(0, input => input.Set(animationNote));
                ServerEssentialSpawner.EnqueueServerMessage(1, input => input.Set(onHitNote));
                ServerEssentialSpawner.EnqueueServerMessage(2, input => input.Set(battleNote));
            }
            else if (_allowToZeroHp)
            {
                // Do rescue action
            }
        }
    }
}