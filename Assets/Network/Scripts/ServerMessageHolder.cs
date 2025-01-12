using System;

namespace Network.Scripts
{
    public class ServerMessageHolder
    {
        internal int MessageOrder { get; }
        internal Action<int> OnServerInputDelivered;

        public ServerMessageHolder(int order)
        {
            MessageOrder = order;
        }

        public void Invoke()
        {
            OnServerInputDelivered?.Invoke(MessageOrder);
        }
    }
}