namespace Tetris
{
    internal class ITetrisGame
    {
        internal ITetrisGlass TetrisGlass { get; set; }

        internal Size FigureSize { get; set; }

        internal Figure CurrentFigure { get; set; }
    }
}
