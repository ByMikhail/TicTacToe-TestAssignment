using TicTacToe.Core.Infrastructure.SceneManagement;
using Zenject;

namespace TicTacToe.Core
{
    public class GameController
    {
        private SceneManager _sceneManager;

        [Inject]
        private void InjectDependencies(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        public void StartNewGame()
        {
            _sceneManager.ReloadCurrentScene();
        }
    }
}