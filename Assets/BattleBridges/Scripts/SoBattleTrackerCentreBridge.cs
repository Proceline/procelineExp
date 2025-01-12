using Commands.Scripts;
using UnityEngine;
using System;
using Network.Scripts;

namespace BattleBridges.Scripts
{
    [CreateAssetMenu(fileName = "BattleTrackerCentreBridge", menuName = "Scriptable Objects/BattleTrackerCentreBridge")]
    public class SoBattleTrackerCentreBridge : ScriptableObject
    {
        private readonly IBattleTracker _battleTracker = new DeliverableBattleTracker();

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
            VovPlayerActions.OnPlayerControllerLoaded += InstantiateLocalUnit;
        }

        public ClientLocalUnit testPrefab;

        public static async void InstantiateLocalUnit(int index)
        {
            var testLayout = FindAnyObjectByType<UnityEngine.UI.GridLayoutGroup>();
            var unit = await ClientLocalUnit.InstantiateLocalUnit(_internalInstance.testPrefab, testLayout.transform,
                0);
            unit.GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText(index.ToString());
        }
        
        public void InformServerAttendedUnit(int playerId, IUnitCondition newUnit)
        {
            uint receivedUniqueId = _battleTracker.AssignHexForUnit(newUnit);
            MarkUnitAssignInfoToSend(playerId, receivedUniqueId, newUnit.UnitIndex);
        }

        /// <summary>
        /// Send Unit info to All players once the unit Added into list
        /// </summary>
        /// <param name="unitOwnerId">PlayerId who owned this unit.</param>
        /// <param name="unitId">UnitId that arranged and assigned by server</param>
        /// <param name="unitIndex">UnitIndex that indicates which unit that player's holding</param>
        private void MarkUnitAssignInfoToSend(int unitOwnerId, uint unitId, int unitIndex)
        {
            
        }
    
    }
}
