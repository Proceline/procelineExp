namespace Commands.Scripts
{
    public interface IActionCommand
    {
        public int GetPlayerId { get; }
        public uint GetActionHexId { get; }
        public uint GetActionOwner { get; }
        public uint GetActionTarget { get; }
    }
    
    public interface IActionCommand<out T, in TV> : IActionCommand
    {
        public T ArchiveResult(TV visitor);
    }
}
