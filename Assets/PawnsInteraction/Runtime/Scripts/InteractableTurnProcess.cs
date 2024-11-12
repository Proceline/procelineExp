using System;
using UnityEngine;

namespace PawnsInteraction.Runtime.Scripts
{
    public class InteractableTurnProcess : MonoBehaviour
    {
        public event Func<ITurnInfo, Awaitable> OnTurnStarted;
        public InteractableActionProcess mainBodyAction;
        public event Func<ITurnInfo, Awaitable> OnTurnFinished;

        public async Awaitable Apply(ITurnInfo turnInfo)
        {
            if (OnTurnStarted != null)
            {
                await OnTurnStarted.Invoke(turnInfo);
            }
            
            await mainBodyAction.DoAction(this);
            
            if (OnTurnFinished != null)
            {
                await OnTurnFinished.Invoke(turnInfo);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Apply(null);
            }
        }
    }
}
