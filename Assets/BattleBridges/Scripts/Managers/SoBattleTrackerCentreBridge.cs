using System;
using System.Collections.Generic;
using BattleBridges.Scripts.Units;
using Commands.Scripts;
using Fusion;
using Network.Scripts;
using UnityEngine;

namespace BattleBridges.Scripts.Managers
{
    [CreateAssetMenu(fileName = "BattleTrackerCentreBridge", menuName = "Scriptable Objects/BattleTrackerCentreBridge")]
    public partial class SoBattleTrackerCentreBridge : ScriptableObject
    {
        private static SoBattleTrackerCentreBridge _internalInstance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeStaticInstance()
        {
            var sourceObjects = Resources.LoadAll<SoBattleTrackerCentreBridge>(string.Empty);
            if (sourceObjects == null || sourceObjects.Length <= 0)
            {
                throw new NullReferenceException($"{nameof(SoBattleTrackerCentreBridge)} Cannot be located!");
            }

            _internalInstance = sourceObjects[0];
        }

        public ClientLocalUnit testPrefab;
        private readonly Dictionary<(int, int), ClientLocalUnit> _assignedLocalUnits = new();
        private readonly List<ClientLocalUnit> _allLocalUnits = new();

        public static IActionClientUnit GetLocalUnit(PlayerRef playerRef, int characterIndex)
        {
            return _internalInstance._assignedLocalUnits.GetValueOrDefault((playerRef.PlayerId, characterIndex));
        }
        
        public static async void InstantiateLocalUnit(int playerId)
        {
            var testLayout = FindAnyObjectByType<UnityEngine.UI.GridLayoutGroup>();
            var unit = await ClientLocalUnit.InstantiateLocalUnit(_internalInstance.testPrefab, playerId,
                testLayout.transform, 0);
            unit.GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText(playerId.ToString());

            _internalInstance._allLocalUnits.Add(unit);
            _internalInstance.InformServerAttendedUnit(playerId, unit);
        }

        public void InformServerAttendedUnit(int playerId, IActionClientUnit newUnit)
        {
            ServerRemoteUnit serverUnit = new ServerRemoteUnit(newUnit);
            if (AssignHexForUnit(serverUnit))
            {
                MarkUnitAssignInfoToSend(playerId, newUnit.GetUnitIndex);
            }
        }

        /// <summary>
        /// Send Unit info to All players once the unit Added into list
        /// </summary>
        /// <param name="unitOwnerId">PlayerId who owned this unit.</param>
        /// <param name="unitIndex">UnitIndex that indicates which unit that player's holding</param>
        private void MarkUnitAssignInfoToSend(int unitOwnerId, int unitIndex)
        {
            var localUnit = _allLocalUnits.Find(target =>
                target.GetUnitHostId == unitOwnerId && target.GetUnitIndex == unitIndex);
            _assignedLocalUnits.Add((unitOwnerId, unitIndex), localUnit);
        }
    
    }
}
