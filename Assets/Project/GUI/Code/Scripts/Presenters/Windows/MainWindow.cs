using TicTacToe.Core;
using TicTacToe.Core.GameModel;
using TicTacToe.Core.GameModel.GameResults;
using TicTacToe.GUI.Configs;
using TicTacToe.GUI.Systems.ModalWindowManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TicTacToe.GUI.Presenters.Windows
{
    internal class MainWindow : MonoBehaviour
    {
        private TMP_Text _playerLabel;
        private Button _restartGameButton;
        private GameController _gameController;
        private ModalWindowManager _modalWindowManager;
        private TicTacToeGame _game;
        private MainConfig _mainConfig;

        [Inject]
        private void InjectDependencies
        (
            TMP_Text playerLabel,
            Button restartGameButton,
            GameController gameController,
            ModalWindowManager modalWindowManager,
            TicTacToeGame game,
            MainConfig mainConfig
        )
        {
            _playerLabel = playerLabel;
            _restartGameButton = restartGameButton;
            _gameController = gameController;
            _modalWindowManager = modalWindowManager;
            _game = game;
            _mainConfig = mainConfig;
        }

        #region Unity lifecycle

        private void OnEnable()
        {
            _restartGameButton.onClick.AddListener(RestartGameButton_OnClick);
            _game.OnPlayerSwitched += Game_OnPlayerSwitched;
            _game.OnCompleted += Game_OnCompleted;
        }


        private void OnDisable()
        {
            _restartGameButton.onClick.RemoveListener(RestartGameButton_OnClick);
            _game.OnPlayerSwitched -= Game_OnPlayerSwitched;
            _game.OnCompleted -= Game_OnCompleted;
        }

        #endregion

        #region Event handlers

        private void RestartGameButton_OnClick()
        {
            _gameController.StartNewGame();
        }

        private void Game_OnPlayerSwitched(Player obj)
        {
            var labelText = "Undefined";
            var labelColor = Color.white;

            switch (obj)
            {
                case Player.X:
                {
                    labelText = "X's turn";
                    labelColor = _mainConfig.PlayerColorX;
                    break;
                }
                case Player.O:
                {
                    labelText = "O's turn";
                    labelColor = _mainConfig.PlayerColorO;
                    break;
                }
                default:
                {
                    Debug.LogError($"[{nameof(MainWindow)}] Passed unknown value '{nameof(Player)}.{obj}'.");
                    break;
                }
            }

            _playerLabel.text = labelText;
            _playerLabel.color = labelColor;
        }

        private void Game_OnCompleted(IGameResult gameResult)
        {
            _playerLabel.enabled = false;
            ShowGameResultWindow(gameResult);
        }

        #endregion

        private void ShowGameResultWindow(IGameResult gameResult)
        {
            string modalWindowMessage;

            if (gameResult is WinLose winLoseResult)
            {
                modalWindowMessage = winLoseResult.Winner == Player.X ? "X WINS" : "O WINS";
            }
            else
            {
                modalWindowMessage = "TIE";
            }

            _modalWindowManager.Show("Game Result", modalWindowMessage, () => { _gameController.StartNewGame(); });
        }
    }
}