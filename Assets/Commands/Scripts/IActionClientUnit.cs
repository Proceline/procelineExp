namespace Commands.Scripts
{
    public interface IActionClientUnit
    {
        public int GetUnitHostId { get; }
        public int GetUnitIndex { get; }
        public uint GetUnitHexId { get; }
        public uint GetActionHexId(int index);
    }
}