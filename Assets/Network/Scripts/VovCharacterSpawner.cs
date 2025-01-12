using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace Network.Scripts
{
    public class VovCharacterSpawner : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private NetworkPrefabRef playerController;
        private readonly Dictionary<PlayerRef, NetworkObject> _controllers = new();
    
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (runner.IsServer)
            {
                var controller = runner.Spawn(playerController, Vector3.zero, Quaternion.identity, player);
                _controllers.TryAdd(player, controller);
            }
        }
    
        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            if (_controllers.TryGetValue(player, out var controller))
            {
                runner.Despawn(controller);
                _controllers.Remove(player);
            }
            Debug.LogWarning("TBD: Need to hand over to an AI.");
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            if (runner.IsServer && SoBattleManager.IsPlayersCollected)
            {
                Debug.LogWarning("Start analyzing!");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var data = new VovNetInputData { ActionId = 0 };
                Debug.LogWarning("Input Runner is Server: " + runner.IsServer + " " + Input.GetKey(KeyCode.Space));
                data.PlayerIndex = 0;
                data.ActionId = 1;
                input.Set(data);
            }
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
            Debug.LogWarning("Disconnected from server");
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnConnectedToServer(NetworkRunner runner) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
            Debug.LogError("Host Migrated");
        }

        public void OnSceneLoadDone(NetworkRunner runner) { }

        public void OnSceneLoadStart(NetworkRunner runner) { }
        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

    }
}
