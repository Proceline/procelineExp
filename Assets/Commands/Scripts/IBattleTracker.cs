using System.Collections.Generic;

namespace Commands.Scripts
{
    public interface IBattleTracker
    {
        public void GetConditionOfUnit(uint unitHexId);
    }

    public interface IBattleTracker<in T> : IBattleTracker where T : IBattleNote
    {
        public void AddNoteThenApply(T battleNote);
        public void BroadcastNotes();
    }
}