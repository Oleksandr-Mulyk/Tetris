namespace Tetris
{
    internal class ITetrisGame
    {
        internal ITetrisGlass TetrisGlass { get; set; }

        internal Size FigureSize { get; set; }

        internal Figure CurrentFigure { get; set; }

        internal Figure NextFigure { get; set; }

        internal Coordinate FigurePoint { get; set; }

        internal int Score { get; set; }

        internal int Level { get; set; }
    }
}
