using System.Collections.Generic;

namespace TicTacToe.Core.GameModel.GameResults
{
    public readonly struct WinLose : IGameResult
    {
        public readonly Player Winner;
        public readonly IEnumerable<GridCell> WinningMatch;

        public WinLose(Player winner, IEnumerable<GridCell> winningMatch)
        {
            Winner = winner;
            WinningMatch = winningMatch;
        }
    }
}