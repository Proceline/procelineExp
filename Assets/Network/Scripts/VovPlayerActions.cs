using Fusion;
using UnityEngine;

public class VovPlayerActions : NetworkBehaviour
{
    // [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    // public void SendAction(int actionId, NetworkObject target)
    // {
    //     Debug.Log($"Action received: {actionId} on {target.name}");
    //     // Store the action for simulation later
    // }
}