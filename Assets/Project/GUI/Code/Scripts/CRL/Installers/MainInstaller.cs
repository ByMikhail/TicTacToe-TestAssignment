using TicTacToe.GUI.Configs;
using TicTacToe.GUI.Systems.ModalWindowManagement;
using UnityEngine;
using Zenject;

namespace TicTacToe.GUI.CRL.Installers
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private MainConfig _mainConfig;
        [SerializeField] private ModalWindowManager _modalWindowManager;
        [SerializeField] private ModalWindow _modalWindowPrefab;

        public override void InstallBindings()
        {
            Container
                .Bind<MainConfig>()
                .FromInstance(_mainConfig);

            Container
                .Bind<ModalWindowManager>()
                .FromInstance(_modalWindowManager);

            Container
                .BindFactory<ModalWindow, ModalWindowFactory>()
                .FromComponentInNewPrefab(_modalWindowPrefab);
        }
    }
}