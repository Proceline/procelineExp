using UnityEngine;

namespace Commands.Scripts
{
    public interface IUnitCondition
    {
        public int UnitIndex { get; }
        public uint UnitTypeId { get; }
        
        public Vector2 GetHitPoint { get; }
        public bool IsExisted { get; }
        public Vector2 GetMagicPoint { get; }
        public int GetActionPoint { get; }
        public float GetAttackPower { get; }
        public float GetSpiritPower { get; }
        public float GetAttributePoint(string attributeName);

        public void ReceiveDamage(float damageValue, int result);
        public void ReceiveHeal(float healAmount, int result);
    }
}