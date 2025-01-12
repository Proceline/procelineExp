using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Network.Scripts
{
    public class VovPhotonManager : MonoBehaviour
    {
        private NetworkRunner _runner;
        [SerializeField] private NetworkPrefabRef serverAnalyzer;

        public void StartHostGame()
        {
            StartGame(GameMode.AutoHostOrClient);
        }
    
        public void StartClientGame()
        {
            StartGame(GameMode.Client);
        }
    
        private async void StartGame(GameMode mode)
        {
            _runner = gameObject.AddComponent<NetworkRunner>();

            var sceneRef = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
            var sceneInfo = new NetworkSceneInfo();

            if (sceneRef.IsValid)
            {
                sceneInfo.AddSceneRef(sceneRef, LoadSceneMode.Additive);
            }
        
            var startGameArgs = new StartGameArgs
            {
                GameMode = mode, // Or GameMode.Client for clients
                SessionName = "RPGBattleSession", // Name of the session
                Scene = sceneRef,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
            };
        
            await _runner.StartGame(startGameArgs);

            if (_runner.IsServer)
            {
                Debug.Log("Network started as host.");
                _runner.Spawn(serverAnalyzer, Vector3.zero, Quaternion.identity);
            }
        }
    }
}
