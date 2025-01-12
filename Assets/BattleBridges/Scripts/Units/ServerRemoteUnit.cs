using Commands.Scripts;
using UnityEngine;

namespace BattleBridges.Scripts.Units
{
    public class ServerRemoteUnit : IUnitCondition
    {
        internal ServerRemoteUnit(IActionClientUnit unit)
        {
            GetUnitIdentity = (unit.GetUnitHostId, unit.GetUnitIndex);
            UnitTypeId = unit.GetUnitHexId;
            _basicAttackPower = 10;
            _basicSpiritPower = 10;
            GetHitPoint = new Vector2(100, 100);
            GetMagicPoint = new Vector2(0, 5);
            GetActionPoint = 0;
        }

        public (int, int) GetUnitIdentity { get; }
        public uint UnitTypeId { get; }
        
        public Vector2 GetHitPoint { get; set; }
        public Vector2 GetMagicPoint { get; set; }
        public bool IsExisted { get; set; } = true;
        public int GetActionPoint { get; set; }
        public float GetAttackPower => _basicAttackPower;
        public float GetSpiritPower => _basicSpiritPower;

        private float _basicAttackPower;
        private float _basicSpiritPower;
        

        public float GetAttributePoint(string attributeName) => 0;

        public void ReceiveDamage(float damageValue, int result)
        {
            Debug.Log($"Calculate <color=red>{damageValue}</color> in server");
        }

        public void ReceiveHeal(float healAmount, int result)
        {
            Debug.Log($"Calculate <color=green>{healAmount}</color> in server");
        }
    }
}