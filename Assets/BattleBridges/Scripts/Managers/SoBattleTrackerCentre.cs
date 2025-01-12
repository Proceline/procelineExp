using System.Collections.Generic;
using BattleBridges.Scripts.Commands;
using Commands.Scripts;
using Network.Scripts;
using UnityEngine;

namespace BattleBridges.Scripts.Managers
{
    public partial class SoBattleTrackerCentreBridge : IBattleTracker<ServerActionCommand>
    {
        private readonly Dictionary<(int, int), IUnitCondition> _unitsInCurrentBattle = new();
        private readonly List<ServerActionCommand> _commands = new();

        public IUnitCondition GetConditionOfUnit((int, int) identity)
        {
            return _unitsInCurrentBattle.GetValueOrDefault(identity);
        }

        public bool AssignHexForUnit(IUnitCondition serverUnit) =>
            _unitsInCurrentBattle.TryAdd(serverUnit.GetUnitIdentity, serverUnit);

        public static void OnServerCommandReceived(ServerActionCommand command)
        {
            _internalInstance.AddCommand(command);
            
            // TODO: Remember to REMOVE!
            _internalInstance.AnalyzeNextCommand();
            ServerEssentialSpawner.AllMessageCollected = true;
            // End TODO
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
            serverCommand.ArchiveResult(this);
            Debug.LogError("Server analyzed Command");
        }
    }
}