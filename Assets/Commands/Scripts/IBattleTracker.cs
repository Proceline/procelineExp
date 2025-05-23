namespace Commands.Scripts
{
    public interface IBattleTracker
    {
        public IUnitCondition GetConditionOfUnit((int, int) identity);
        public bool AssignHexForUnit(IUnitCondition newUnit);
    }

    public interface IBattleTracker<in T> : IBattleTracker where T : IActionCommand
    {
        public void AddCommand(T command);
        
        /// <summary>
        /// Add Command while normally triggered by other command, will not go prior than existed command if two commands have same priority
        /// </summary>
        /// <param name="command"></param>
        public void AddSortedCommand(T command);
        public void InsertCommandAt(T command, int index);
        public void PushCommand(T command, bool existed);
        public void AnalyzeNextCommand();
    }
}