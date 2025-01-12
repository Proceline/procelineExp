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