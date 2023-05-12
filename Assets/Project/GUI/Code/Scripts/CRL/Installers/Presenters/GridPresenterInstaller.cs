using TicTacToe.GUI.Views;
using TicTacToe.GUI.Views.Grid;
using UnityEngine;
using Zenject;

namespace TicTacToe.GUI.CRL.Installers.Presenters
{
    internal class GridPresenterInstaller : MonoInstaller
    {
        [SerializeField] private GridView _gridView;
        [SerializeField] private MarkView _markViewPrefab;

        public override void InstallBindings()
        {
            Container
                .Bind<GridView>()
                .FromInstance(_gridView);

            Container
                .BindFactory<MarkView, MarkViewFactory>()
                .FromComponentInNewPrefab(_markViewPrefab);
        }
    }
}