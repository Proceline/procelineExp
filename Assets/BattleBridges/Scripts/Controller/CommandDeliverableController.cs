using BattleBridges.Scripts.BattleNotes;
using BattleBridges.Scripts.Managers;
using Fusion;
using UnityEngine;

namespace BattleBridges.Scripts.Controller
{
    public class CommandDeliverableController : NetworkBehaviour
    {
        
        private void Start()
        {
            SoBattleTrackerCentreBridge.InstantiateLocalUnit(Object.InputAuthority.PlayerId);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) && Object.HasInputAuthority)
            {
                RpcOnClientCommandReceivedByServer();
            }
        }

        public override void FixedUpdateNetwork()
        {
            var hello = Runner.GetInputForPlayer<BattleNoteAnimationInput>(Object.InputAuthority);
            Debug.Log(((BattleNoteAnimationInput)hello).AnimatingCharacter);
            if (GetInput(out BattleNoteAnimationInput animInput))
            {
                Debug.Log($"{animInput.AnimatingCharacter} has acted animation {animInput.AnimationHexId}, at time {animInput.Timeline}");
            }

            if (GetInput(out BattleNoteAttributeValueInput valueInput))
            {
                
                Debug.Log($"{valueInput.FromUnit} has applied value {valueInput.DeltaValue} to {valueInput.TargetUnit}" +
                          $", result HitPoint is {valueInput.AfterValue} on Attribute {valueInput.AttributeFieldId}");
            }
        }
        
        /// <summary>
        /// Send Command Response for Server, only Server can receive the details
        /// </summary>
        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        private void RpcOnClientCommandReceivedByServer()
        {
            if (!Runner.IsServer) return;
            PlayerRef playerRef = Object.InputAuthority;
            BattleSupportActionsLibrary.DeliveryActionCommand(playerRef, 0);
        }
    }
}