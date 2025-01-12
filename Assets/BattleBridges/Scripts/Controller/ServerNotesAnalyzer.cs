using System;
using System.Collections.Generic;
using Fusion;
using Network.Scripts;
using UnityEngine;

namespace BattleBridges.Scripts.Controller
{
    public class ServerNotesAnalyzer : NetworkBehaviour
    {
        private bool _allMessageCollected;
        private static readonly Queue<ServerMessageHolder> PendingMessages = new();
        
        /// <summary>
        /// Used for resend the action, but also can be used as Pool
        /// </summary>
        private static readonly List<ServerMessageHolder> UsedServerMessages = new();

        private void Update()
        {
            OnPendingMessagesTick();
        }

        public void PrepareForMessageDelivery()
        {
            _allMessageCollected = true;
        }
        
        public static void EnqueueServerMessage(int messageOrder, Action<int> function)
        {
            var action = new ServerMessageHolder(messageOrder)
            {
                OnServerInputDelivered = function
            };
            PendingMessages.Enqueue(action);
        }
        
        private void OnPendingMessagesTick()
        {
            if (!Object.HasStateAuthority) return; // Ensure this runs only on the server
            
            if (_allMessageCollected)
            {
                if (PendingMessages.Count > 0)
                {
                    var pendingAction = PendingMessages.Dequeue();
                    pendingAction?.Invoke();
                    UsedServerMessages.Add(pendingAction);
                }

                // Check possible missing delivered action
                if (PendingMessages.Count == 0)
                {
                    _allMessageCollected = false;
                }
            }
        }
        
        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        public void RpcSendAnimationNote(int orderIndex, int timeline, Vector2Int unit, uint animationId)
        {
            Debug.Log($"{orderIndex}: Unit_{unit} has acted animation {animationId}, at time {timeline}");

            // if (GetInput(out BattleNoteAttributeValueInput valueInput))
            // {
            //     
            //     Debug.Log($"{valueInput.FromUnit} has applied value {valueInput.DeltaValue} to {valueInput.TargetUnit}" +
            //               $", result HitPoint is {valueInput.AfterValue} on Attribute {valueInput.AttributeFieldId}");
            // }
        }

        /// <summary>
        /// Send Action Note first case: Send a value modification note to client
        /// </summary>
        /// <param name="orderIndex">The order to trace if any Note missing</param>
        /// <param name="timeline">Time stamp the action will on</param>
        /// <param name="unit">Action caster</param>
        /// <param name="target">Action victim</param>
        /// <param name="actionId">Action Hex Id, eg. 0x00000 means Attack</param>
        /// <param name="valueTuple">The Tuple will be: BeforeValue, AfterValue, DeltaValue</param>
        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        public void RpcSendActionNote(int orderIndex, int timeline, Vector2Int unit, Vector2Int target,
            Vector3Int valueTuple)
        {
            Debug.Log($"{orderIndex}: Unit_{unit} has triggered value on {target}, " +
                      $"at time {timeline}, with value {valueTuple}");
            // if (GetInput(out BattleNoteAttributeValueInput valueInput))
            // {
            //     
            //     Debug.Log($"{valueInput.FromUnit} has applied value {valueInput.DeltaValue} to {valueInput.TargetUnit}" +
            //               $", result HitPoint is {valueInput.AfterValue} on Attribute {valueInput.AttributeFieldId}");
            // }
        }
    }
}