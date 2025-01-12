using System;
using System.Threading.Tasks;
using Commands.Scripts;
using UnityEngine;

namespace BattleBridges.Scripts
{
    public class ClientLocalUnit : MonoBehaviour, IUnitCondition
    {
        public static async Task<ClientLocalUnit> InstantiateLocalUnit(ClientLocalUnit prefab, Transform parent, int index)
        {
            var operation = InstantiateAsync(prefab, parent);
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (operation.Result.Length <= 0) return null;
            
            var result = operation.Result[0];
            result.UnitIndex = index;
            result.ConvertUnitTypeIdFromString();
            return result;

        }

        [SerializeField] private string typeHexId = string.Empty;
        
        public int UnitIndex { get; private set; }
        public uint UnitTypeId { get; private set; }

        private void ConvertUnitTypeIdFromString()
        {
            UnitTypeId = Convert.ToUInt32(typeHexId, 16);
        }
        
        public Vector2 GetHitPoint { get; private set; }
        public bool IsExisted { get; private set; } = true;
        public Vector2 GetMagicPoint { get; }
        public int GetActionPoint { get; }
        public float GetAttackPower { get; }
        public float GetSpiritPower { get; }

        public float GetAttributePoint(string attributeName) => 0;

        public void ReceiveDamage(float damageValue, int result)
        {
            Debug.Log($"Take Damage <color=red>{damageValue}</color>, Hp now is {result}");
        }

        public void ReceiveHeal(float healAmount, int result)
        {
            Debug.Log($"Get Healed <color=green>{healAmount}</color>, Hp now is {result}");
        }
    }
}