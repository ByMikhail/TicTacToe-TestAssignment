using TicTacToe.Core.GameModel;
using TicTacToe.Core.GameModel.GameResults;
using TicTacToe.GUI.Configs;
using TicTacToe.GUI.Views;
using TicTacToe.GUI.Views.Grid;
using UnityEngine;
using Zenject;

namespace TicTacToe.GUI.Presenters.Grid
{
    internal class GridPresenter : MonoBehaviour
    {
        private MainConfig _mainConfig;
        private TicTacToeGame _game;
        private GridView _gridView;
        private MarkViewFactory _markViewFactory;

        [Inject]
        private void InjectDependencies
        (
            MainConfig mainConfig,
            TicTacToeGame game,
            GridView gridView,
            MarkViewFactory markViewFactory
        )
        {
            _mainConfig = mainConfig;
            _game = game;
            _gridView = gridView;
            _markViewFactory = markViewFactory;
        }

        #region Unity lifecycle

        private void Awake()
        {
            _gridView.Init(_game.GridColumnsCount, _game.GridRowsCount);
        }

        private void OnEnable()
        {
            _game.OnCompleted += Game_OnCompleted;
            _game.OnMarkPlaced += Game_OnMarkPlaced;

            foreach (var gridCell in _gridView)
            {
                gridCell.OnClick += CellView_OnClick;
            }
        }

        private void OnDisable()
        {
            _game.OnCompleted -= Game_OnCompleted;
            _game.OnMarkPlaced += Game_OnMarkPlaced;

            foreach (var gridCell in _gridView)
            {
                gridCell.OnClick -= CellView_OnClick;
            }
        }

        #endregion

        #region Event handlers

        private void CellView_OnClick(IGridCellView cellView)
        {
            var cellPosition = cellView.Position;

            PlaceMark(cellPosition);
            cellView.Interactable = false;
        }

        private void Game_OnMarkPlaced(GridCell gridCell)
        {
            var markHolder = _gridView[new Vector2Int(gridCell.X, gridCell.Y)].ContentHolder;
            var markOwner = _game.WhoseMarkIsInCell(gridCell);

            var markView = CreateMarkView(markHolder);
            markView.Color = GetMarkColorForPlayer(markOwner);
        }

        private void Game_OnCompleted(IGameResult gameResult)
        {
            if (gameResult is WinLose winLoseResult)
            {
                foreach (var gridCell in winLoseResult.WinningMatch)
                {
                    _gridView[new Vector2Int(gridCell.X, gridCell.Y)].IsFilled = true;
                }
            }

            foreach (var cellView in _gridView)
            {
                cellView.Interactable = false;
            }
        }

        #endregion

        private void PlaceMark(Vector2Int position)
        {
            var gridCell = new GridCell(position.x, position.y);
            _game.PlaceMark(gridCell);
        }

        private MarkView CreateMarkView(Transform holder)
        {
            var markView = _markViewFactory.Create();
            markView.transform.SetParent(holder, false);

            return markView;
        }

        private Color GetMarkColorForPlayer(Player player)
        {
            return player == Player.X ? _mainConfig.PlayerColorX : _mainConfig.PlayerColorO;
        }
    }
}