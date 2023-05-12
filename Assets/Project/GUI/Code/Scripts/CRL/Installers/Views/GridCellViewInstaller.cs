using TicTacToe.GUI.Views.Grid;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TicTacToe.GUI.CRL.Installers.Views
{
    public class GridCellViewInstaller : MonoInstaller
    {
        [SerializeField] private RectTransform _contentHolder;
        [SerializeField] private Image _fillImage;

        public override void InstallBindings()
        {
            Container
                .Bind<RectTransform>()
                .FromInstance(_contentHolder)
                .WhenInjectedInto<GridCellView>();

            Container
                .Bind<Image>()
                .FromInstance(_fillImage)
                .WhenInjectedInto<GridCellView>();
        }
    }
}