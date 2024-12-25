using Fusion;
namespace NetworkLogic.Scripts
{
    public struct VovCommandInput : INetworkInput
    {
        public uint BattleActorId;
        public VovCommandMainType Type;
        public int ExtraIndex;
    }
}
