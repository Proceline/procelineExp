using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace Network.Scripts
{
    public class ServerEssentialSpawner : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private NetworkPrefabRef playerController;
        private readonly Dictionary<PlayerRef, NetworkObject> _controllers = new();

        public static bool AllMessageCollected { get; set; } = false;
        private static readonly Queue<ServerMessageAction> PendingMessageActions = new();
        
        /// <summary>
        /// Used for resend the action, but also can be used as Pool
        /// </summary>
        private static readonly List<ServerMessageAction> UsedServerMessageCollection = new();

        public static void EnqueueServerMessage(int messageOrder, Action<NetworkInput> function)
        {
            var action = new ServerMessageAction(messageOrder)
            {
                OnServerInputDelivered = function
            };
            PendingMessageActions.Enqueue(action);
        }
        
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
            if (runner.IsServer)
            {
                var animationNote = new BattleBridges.Scripts.BattleNotes.BattleNoteAnimationInput
                {
                    AnimatingCharacter = new Vector2Int(2, 2),
                    AnimationHexId = 0x0000,
                    Timeline = 0
                };
                input.Set(animationNote);
            }
            if (AllMessageCollected)
            {
                if (runner.IsServer && PendingMessageActions.Count > 0)
                {
                    var pendingAction = PendingMessageActions.Dequeue();
                    pendingAction?.Invoke(input);
                    UsedServerMessageCollection.Add(pendingAction);
                }

                // Check possible missing delivered action
                if (PendingMessageActions.Count == 0)
                {
                    foreach (var message in UsedServerMessageCollection)
                    {
                        PendingMessageActions.Enqueue(message);
                    }
                    // AllMessageCollected = false;
                }
            }

            if (!runner.IsServer && PendingMessageActions.Count > 0)
            {
                Debug.LogError("Server and Client Mixed Up!");
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
