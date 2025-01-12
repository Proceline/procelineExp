namespace Commands.Scripts
{
    public interface IActionCommand
    {
        public uint GetActionHexId { get; }
        public (int, int) GetActionOwner { get; }
        public (int, int) GetActionTarget { get; }
    }
    
    public interface IActionCommand<in T> : IActionCommand
    {
        public void ArchiveResult(T visitor);
    }
}
