using System;
using Fusion;
using UnityEngine;

namespace Network.Scripts
{
    public class VovPlayerActions : NetworkBehaviour
    {
        public static event Action<int> OnPlayerControllerLoaded;
        
        private void Start()
        {
            OnPlayerControllerLoaded?.Invoke(Object.InputAuthority.PlayerId);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) && Object.HasInputAuthority)
            {
                RpcOnServerCommandReceived(Object.InputAuthority);
            }
        }

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out VovNetInputData data))
            {
                Debug.Log("Received Data! " + data.ActionId);
            }
        }
    
        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RpcOnServerCommandReceived(PlayerRef playerRef)
        {
            if (!Runner.IsServer) return;
            Debug.Log($"Player {playerRef.PlayerId} action Delivered!");
            SoBattleManager.AddPlayer(playerRef.PlayerId);
        }
    }
}