using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TicTacToe.GUI.Views.Grid
{
    [RequireComponent(typeof(GridLayoutGroup))]
    internal class GridView : MonoBehaviour, IEnumerable<IGridCellView>
    {
        public IGridCellView this[Vector2Int position] => _gridCells[position.x, position.y];

        private IGridCellView[,] _gridCells;
        private GridCellViewFactory _gridCellViewFactory;

        [Inject]
        private void InjectDependencies(GridCellViewFactory gridCellViewFactory)
        {
            _gridCellViewFactory = gridCellViewFactory;
        }

        public void Init(int width, int height)
        {
            _gridCells = new IGridCellView[width, height];

            PopulateGridWithCells();
        }

        public IEnumerator<IGridCellView> GetEnumerator()
        {
            foreach (var gridCellView in _gridCells)
            {
                yield return gridCellView;
            }
        }

        private void PopulateGridWithCells()
        {
            for (int row = 0; row < _gridCells.GetLength(1); row++)
            {
                for (int column = 0; column < _gridCells.GetLength(0); column++)
                {
                    var cellView = _gridCellViewFactory.Create();
                    cellView.transform.SetParent(transform, false);
                    cellView.Init(new Vector2Int(column, row));

                    _gridCells[column, row] = cellView;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}