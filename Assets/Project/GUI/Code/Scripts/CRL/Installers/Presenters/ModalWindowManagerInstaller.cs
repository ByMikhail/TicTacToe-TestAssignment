using UnityEngine;
using Zenject;

namespace TicTacToe.GUI.CRL.Installers.Presenters
{
    public class ModalWindowManagerInstaller : MonoInstaller
    {
        [SerializeField] private Transform _modalWindowContainer;

        public override void InstallBindings()
        {
            Container
                .Bind<Transform>()
                .FromInstance(_modalWindowContainer);
        }
    }
}