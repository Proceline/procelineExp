using System;
using UnityEngine;

namespace PawnsInteraction.Runtime.Scripts
{
    public class InteractableActionProcess : MonoBehaviour
    {
        public event Func<InteractableTurnProcess, Awaitable> BeforeActionCalled;
        public event Func<InteractableTurnProcess, Awaitable> OnActionCalled;
        public event Func<InteractableTurnProcess, Awaitable> OnActionFinished;

        public async Awaitable DoAction(InteractableTurnProcess turnProcess)
        {
            if (BeforeActionCalled != null)
            {
                await BeforeActionCalled.Invoke(turnProcess);
            }

            if (OnActionCalled != null)
            {
                await OnActionCalled.Invoke(turnProcess);
            }

            if (OnActionFinished != null)
            {
                await OnActionFinished.Invoke(turnProcess);
            }
        }
        
        private void Start()
        {
            OnActionCalled += TestActiveAttack;
            OnActionFinished += CounterAttack;
        }
        
        private async Awaitable TestActiveAttack(InteractableTurnProcess turnProcess)
        {
            await TestAttack("Proco", "Bobby", "Attack", 10);
        }
        
        private async Awaitable CounterAttack(InteractableTurnProcess turnProcess)
        {
            await TestAttack("Bobby", "Proco", "Attack", 10);
        }
        
        private async Awaitable TestAttack(string caster, string victim, string behavior, int value)
        {
            Debug.Log($"<{caster}> started [{behavior}]");
            await Awaitable.WaitForSecondsAsync(0.5f);
            Debug.Log($"<{caster}> did [{behavior}] on <{victim}>, with [{value}]");
        }
    }
}
