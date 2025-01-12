using Fusion;
using UnityEngine;

namespace BattleBridges.Scripts.Controller
{
    public class ServerCommandsAnalyzer : NetworkBehaviour
    {
        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        public void RpcSendAnimationData(int orderIndex, Vector2Int timeline)
        {
        }
        
        // public void SendAnimationToClients() {
        //     if (!Object.HasStateAuthority) return; // Ensure this runs only on the server
        //
        //     
        // public Vector2Int FromUnit;
        // public Vector2Int TargetUnit;
        // public uint AttributeFieldId;
        // public int BeforeValue;
        // public int AfterValue;
        // public int DeltaValue;
        // public int Timeline;
        //     var animatingCharacter = new Vector2Int(2, 2);
        //     var animationHexId = 0x0000;
        //     var timeline = 0;
        //
        //     RpcSendAnimationData(animatingCharacter, animationHexId, timeline); // Broadcast to clients
        // }
    }

    public struct Test
    {
        private Vector2Int index;
    }
}