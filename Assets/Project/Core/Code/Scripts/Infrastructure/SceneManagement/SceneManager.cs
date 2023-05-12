using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace TicTacToe.Core.Infrastructure.SceneManagement
{
    public class SceneManager
    {
        public void ReloadCurrentScene()
        {
            var currentScene = UnitySceneManager.GetActiveScene();
            UnitySceneManager.LoadScene(currentScene.buildIndex);
        }
    }
}