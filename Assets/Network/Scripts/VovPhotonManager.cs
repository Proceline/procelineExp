using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VovPhotonManager : MonoBehaviour
{
    private NetworkRunner _runner;

    async void Start()
    {
        _runner = gameObject.AddComponent<NetworkRunner>();

        var sceneRef = SceneRef.FromIndex(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        var sceneInfo = new NetworkSceneInfo();

        if (sceneRef.IsValid)
        {
            sceneInfo.AddSceneRef(sceneRef, LoadSceneMode.Additive);
        }
        
        var startGameArgs = new StartGameArgs
        {
            GameMode = GameMode.Host, // Or GameMode.Client for clients
            SessionName = "RPGBattleSession", // Name of the session
            Scene = sceneRef,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        };

        await _runner.StartGame(startGameArgs);
        Debug.Log("Network started as host.");
    }
}
