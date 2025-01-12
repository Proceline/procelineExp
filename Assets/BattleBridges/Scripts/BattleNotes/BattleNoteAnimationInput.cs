using Fusion;
using UnityEngine;

namespace BattleBridges.Scripts.BattleNotes
{
    public struct BattleNoteAnimationInput : INetworkInput
    {
        public Vector2Int AnimatingCharacter;
        
        /// <summary>
        /// Can be translated into milliseconds
        /// </summary>
        public int Timeline;
        
        /// <summary>
        /// For example, 0x00000 is Attack, while 0x00000 is Hit
        /// </summary>
        public uint AnimationHexId;
    }
}