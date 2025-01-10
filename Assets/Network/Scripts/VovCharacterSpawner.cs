using UnityEngine;
using Fusion;

public class VovCharacterSpawner : MonoBehaviour
{
    public NetworkObject playerPrefab;

    public void SpawnPlayer(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
            runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);
        }
    }
}
