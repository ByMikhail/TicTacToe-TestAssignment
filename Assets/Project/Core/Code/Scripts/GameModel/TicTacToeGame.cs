using System;

namespace TicTacToe.Core.GameModel
{
    public class TicTacToeGame
    {
        public int GridRowsCount => _gridMarks.GetLength(1);
        public int GridColumnsCount => _gridMarks.GetLength(0);
        public bool GridHasAvailableCells => _marksCount < _gridMarks.Length;
        public bool IsCompleted { get; private set; }
        public GameResult Result { get; private set; }

        public event Action<GridCell> OnMarkPlaced;
        public event Action<Player> OnPlayerSwitched;
        public event Action<GameResult> OnCompleted;

        private Player?[,] _gridMarks;
        private int _marksCount;
        private Player _currentPlayer;

        private static GridCell[][] _winningMatches;

        public TicTacToeGame()
        {
            _gridMarks = new Player?[3, 3];
            _marksCount = 0;
            _currentPlayer = Player.X;
            IsCompleted = false;
            Result = null;
        }

        static TicTacToeGame()
        {
            _winningMatches = new[]
            {
                new[] { new GridCell(0, 0), new GridCell(1, 0), new GridCell(2, 0) },
                new[] { new GridCell(0, 0), new GridCell(1, 0), new GridCell(2, 0) },
                new[] { new GridCell(0, 1), new GridCell(1, 1), new GridCell(2, 1) },
                new[] { new GridCell(0, 0), new GridCell(0, 1), new GridCell(0, 2) },
                new[] { new GridCell(1, 0), new GridCell(1, 1), new GridCell(1, 2) },
                new[] { new GridCell(2, 0), new GridCell(2, 1), new GridCell(2, 2) },
                new[] { new GridCell(0, 0), new GridCell(1, 1), new GridCell(2, 2) },
                new[] { new GridCell(2, 0), new GridCell(1, 1), new GridCell(0, 2) }
            };
        }

        #region Public Methods

        public void PlaceMark(GridCell cell)
        {
            if (CellContainsMark(cell))
            {
                throw new ArgumentException($"[{nameof(TicTacToeGame)}] Mark cannot be placed. Cell already contains mark. Use '{nameof(CellContainsMark)}' to check.");
            }

            _gridMarks[cell.X, cell.Y] = _currentPlayer;
            _marksCount++;
            OnMarkPlaced?.Invoke(cell);

            SwitchCurrentPlayer();
            CheckCompletionState();
        }

        public bool CellContainsMark(GridCell cell)
        {
            return _gridMarks[cell.X, cell.Y].HasValue;
        }

        public Player WhoseMarkIsInCell(GridCell cell)
        {
            if (!CellContainsMark(cell))
            {
                throw new ArgumentException($"[{nameof(TicTacToeGame)}] Cell doesn't contain mark. Use '{nameof(CellContainsMark)}' to check.");
            }

            return _gridMarks[cell.X, cell.Y].Value;
        }

        #endregion


        #region Private Methods

        private void Complete(GameResult gameResult)
        {
            IsCompleted = true;
            Result = gameResult;
            OnCompleted?.Invoke(Result);
        }

        private void CheckCompletionState()
        {
            if (WinnerCanBeDetermined(out Player winner))
            {
                Complete(GameResult.WinLose(winner));
            }
            else if (!GridHasAvailableCells)
            {
                Complete(GameResult.Tie());
            }
        }

        private bool WinnerCanBeDetermined(out Player winner)
        {
            winner = Player.X;

            foreach (var winningMatch in _winningMatches)
            {
                GridCell firstCellInMatch = winningMatch[0];

                if (CellContainsMark(firstCellInMatch) && CellMarksMatch(winningMatch))
                {
                    winner = WhoseMarkIsInCell(firstCellInMatch);
                    return true;
                }
            }

            return false;
        }

        private bool CellMarksMatch(GridCell[] cells)
        {
            if (cells.Length > 1)
            {
                GridCell previousCell = cells[0];
                Player? previousMark = _gridMarks[previousCell.X, previousCell.Y];

                for (int i = 1; i < cells.Length; i++)
                {
                    var currentCell = cells[i];
                    var currentMark = _gridMarks[currentCell.X, currentCell.Y];

                    if (currentMark != previousMark)
                    {
                        return false;
                    }

                    previousMark = currentMark;
                }
            }

            return true;
        }

        private void SwitchCurrentPlayer()
        {
            _currentPlayer = _currentPlayer == Player.X ? Player.O : Player.X;
            OnPlayerSwitched?.Invoke(_currentPlayer);
        }

        #endregion
    }
}