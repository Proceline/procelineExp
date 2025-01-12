using System;
using System.Collections.Generic;
using UnityEngine;

namespace Commands.Scripts
{
    public class DeliverableBattleTracker : IBattleTracker<ServerActionCommand>
    {
        private readonly Queue<IBattleNote> _battleNotes = new();
        private readonly Dictionary<uint, IUnitCondition> _unitsInCurrentBattle = new();
        private readonly List<ServerActionCommand> _commands = new();
        private uint _recordedUniqueId = 0;

        public IUnitCondition GetConditionOfUnit(uint unitHexId)
        {
            return _unitsInCurrentBattle.GetValueOrDefault(unitHexId);
        }

        public uint AssignHexForUnit(IUnitCondition newUnit)
        {
            _unitsInCurrentBattle.Add(_recordedUniqueId, newUnit);
            uint currentKey = _recordedUniqueId++;
            return currentKey;
        }

        public void AddCommand(ServerActionCommand command)
        {
            _commands.Add(command);
            _commands.Sort();
        }

        public void AddSortedCommand(ServerActionCommand command)
        {
            for (var i = 0; i < _commands.Count; i++)
            {
                if (_commands[i].CompareTo(command) >= 0) continue;
                _commands.Insert(i, command);
                return;
            }
        }

        public void InsertCommandAt(ServerActionCommand command, int index)
        {
            _commands.Insert(index, command);
        }

        public void PushCommand(ServerActionCommand command, bool existed)
        {
            if (existed)
            {
                _commands.Remove(command);
            }
            _commands.Add(command);
        }

        public void AnalyzeNextCommand()
        {
            int lastIndex = _commands.Count - 1;
            var serverCommand = _commands[lastIndex];
            _commands.RemoveAt(lastIndex);
            AddNote(serverCommand.ArchiveResult(this));
        }

        public void AddNote(IBattleNote battleNote)
        {
            _battleNotes.Enqueue(battleNote);
        }

        public void BroadcastNotes()
        {
            Debug.Log(_battleNotes.Count);
        }
    }
}