using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using UnityEngine;

namespace Network.Scripts
{
    [CreateAssetMenu(fileName = "SoBattleManager", menuName = "Scriptable Objects/SoBattleManager")]
    public class SoBattleManager : ScriptableObject
    {
        private static SoBattleManager _internalInstance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeStaticInstance()
        {
            var sourceObjects = Resources.LoadAll<SoBattleManager>(String.Empty);
            if (sourceObjects == null || sourceObjects.Length <= 0)
            {
                throw new NullReferenceException($"{nameof(SoBattleManager)} Cannot be located!");
            }

            _internalInstance = sourceObjects[0];
        }

        private static readonly List<int> PlayerIds = new();

        public static void AddPlayer(int playerId)
        {
            if (!PlayerIds.Contains(playerId)) PlayerIds.Add(playerId);
        }

        public static bool IsPlayersCollected => PlayerIds.Count >= 1;
    }
}
