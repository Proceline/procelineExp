using System;
using Fusion;

namespace Network.Scripts
{
    public class ServerMessageAction
    {
        internal int MessageOrder { get; }
        internal Action<NetworkInput> OnServerInputDelivered;

        public ServerMessageAction(int order)
        {
            MessageOrder = order;
        }

        public void Invoke(NetworkInput networkInput)
        {
            OnServerInputDelivered?.Invoke(networkInput);
        }
    }
}