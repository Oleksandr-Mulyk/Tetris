namespace Tetris
{
    internal interface ITetrisGame
    {
        public ITetrisGlass TetrisGlass { get; set; }

        public Size FigureSize { get; set; }

        public Figure CurrentFigure { get; set; }

        public Figure NextFigure { get; set; }

        public Coordinate FigurePoint { get; set; }

        public int Score { get; set; }

        public int Level { get; set; }
    }
}
