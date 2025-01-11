using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class ActionManager : MonoBehaviour
{
    private List<ActionCommand> actionQueue = new List<ActionCommand>();

    public void AddAction(ActionCommand command)
    {
        actionQueue.Add(command);
        if (actionQueue.Count == 6)
        {
            // Wait for all players (3v3 scenario)
            ProcessActions();
        }
    }

    private void ProcessActions()
    {
        actionQueue.Sort((a, b) => b.speed.CompareTo(a.speed)); // Sort by Speed
        foreach (var action in actionQueue)
        {
            action.Execute();
        }

        SendResultsToClients();
        actionQueue.Clear();
    }

    private void SendResultsToClients()
    {
        BroadcastResults(new int[] { 2, 1, 2 });
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void BroadcastResults(int[] healthValues)
    {
        for (int i = 0; i < healthValues.Length; i++)
        {
            Debug.Log($"Player {i + 1} health: {healthValues[i]}");
            // Update player health UI or animations here
        }
    }
}

public class ActionCommand
{
    public int speed;
    public GameObject target;

    public void Execute()
    {
        // Apply damage or effects
    }
}