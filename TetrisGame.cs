namespace Tetris
{
    internal class TetrisGame : ITetrisGame
    {
        public ITetrisGlass TetrisGlass { get; set; }

        public Size FigureSize { get; set; }

        public Figure CurrentFigure { get; set; }

        public Figure NextFigure { get; set; }

        public Coordinate FigurePoint { get; set; }

        public int Score { get; set; }

        public int Level { get; set; }

        internal TetrisGame()
        {
            TetrisGlass = new TetrisGlass();
            FigureSize = new(4, 4);
            FigurePoint = new(4, -2);
        }
    }
}
