using System.Collections.Generic;

namespace TicTacToe.Core.GameModel
{
    public sealed class GameResult
    {
        private enum ResultType
        {
            WinLose,
            Tie
        }

        private readonly ResultType _type;
        private readonly Player? _winner;

        private static readonly GameResult _tieResult = new GameResult(ResultType.Tie);
        private static readonly Dictionary<Player, GameResult> _winLoseResults = new Dictionary<Player, GameResult>();

        private GameResult(ResultType resultType, Player? winner = null)
        {
            _type = resultType;
            _winner = winner;
        }

        public static GameResult Tie()
        {
            return _tieResult;
        }

        public static GameResult WinLose(Player winner)
        {
            if (!_winLoseResults.ContainsKey(winner))
            {
                _winLoseResults[winner] = new GameResult(ResultType.WinLose, winner);
            }

            return _winLoseResults[winner];
        }
    }
}