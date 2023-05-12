using TicTacToe.GUI.Views.Grid;
using UnityEngine;
using Zenject;

namespace TicTacToe.GUI.CRL.Installers.Views
{
    public class GridViewInstaller : MonoInstaller
    {
        [SerializeField] private GridCellView _gridCellViewPrefab;

        public override void InstallBindings()
        {
            Container
                .BindFactory<GridCellView, GridCellViewFactory>()
                .FromComponentInNewPrefab(_gridCellViewPrefab);
        }
    }
}