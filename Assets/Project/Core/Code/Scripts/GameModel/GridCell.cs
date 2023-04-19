namespace TicTacToe.Core.GameModel
{
    public readonly struct GridCell
    {
        public readonly int X;
        public readonly int Y;

        public GridCell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}