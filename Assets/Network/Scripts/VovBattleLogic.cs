using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace NetworkLogic.Scripts
{
    public enum VovCommandMainType
    {
        Nothing, Attack, Defend
    }
    
    public class VovBattleLogic : NetworkBehaviour
    {
        // [Networked] private List<VovCommandInput> PlayerCommands { get; set; }
        [Networked, Capacity(10)] private NetworkArray<float> NetworkArray => default;
        private NetworkRunner _runner;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public override void FixedUpdateNetwork()
        {
            if (!_runner.IsServer) return;
            if (GetInput(out VovCommandInput input))
            {
                
            }
        }
    }
}
