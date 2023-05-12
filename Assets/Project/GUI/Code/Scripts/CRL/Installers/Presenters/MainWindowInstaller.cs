using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TicTacToe.GUI.CRL.Installers.Presenters
{
    public class MainWindowInstaller : MonoInstaller
    {
        [SerializeField] private TMP_Text _playerLabel;
        [SerializeField] private Button _restartGameButton;

        public override void InstallBindings()
        {
            Container
                .Bind<TMP_Text>()
                .FromInstance(_playerLabel);

            Container
                .Bind<Button>()
                .FromInstance(_restartGameButton);
        }
    }
}