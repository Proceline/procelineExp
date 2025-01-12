using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Commands.Scripts;
using UnityEngine;

namespace BattleBridges.Scripts.Units
{
    public class ClientLocalUnit : MonoBehaviour, IActionClientUnit
    {
        public static async Task<ClientLocalUnit> InstantiateLocalUnit(ClientLocalUnit prefab, int playerId, Transform parent, int index)
        {
            var operation = InstantiateAsync(prefab, parent);
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (operation.Result.Length <= 0) return null;
            
            var result = operation.Result[0];
            result.GetUnitHostId = playerId;
            result.GetUnitIndex = index;
            result.ConvertUnitTypeIdFromString();
            return result;

        }

        [SerializeField] private string typeHexId = string.Empty;
        private readonly List<uint> _arrangedActions = new() { 0 };
        

        public int GetUnitHostId { get; private set; }
        public int GetUnitIndex { get; private set; }
        public uint GetUnitHexId { get; private set; }
        public uint GetActionHexId(int index)
        {
            return _arrangedActions.Count > index? _arrangedActions[index] : 0x00000;
        }

        private void ConvertUnitTypeIdFromString()
        {
            GetUnitHexId = Convert.ToUInt32(typeHexId, 16);
        }
    }
}