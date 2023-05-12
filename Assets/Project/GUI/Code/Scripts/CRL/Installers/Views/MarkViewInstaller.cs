using TicTacToe.GUI.Views;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TicTacToe.GUI.CRL.Installers.Views
{
    public class MarkViewInstaller : MonoInstaller
    {
        [SerializeField] private Image _markImage;

        public override void InstallBindings()
        {
            Container
                .Bind<Image>()
                .FromInstance(_markImage)
                .WhenInjectedInto<MarkView>();
        }
    }
}