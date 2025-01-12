using Fusion;
using UnityEngine;

namespace BattleBridges.Scripts.BattleNotes
{
    public struct BattleNoteAttributeValueInput : INetworkInput
    {
        public Vector2Int FromUnit;
        public Vector2Int TargetUnit;
        public uint AttributeFieldId;
        public int BeforeValue;
        public int AfterValue;
        public int DeltaValue;
        public int Timeline;
    }
}