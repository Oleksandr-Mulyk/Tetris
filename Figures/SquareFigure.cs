namespace Tetris.Figures
{
    internal class SquareFigure : Figure
    {
        internal SquareFigure()
        {
            Coordinates = [(1,1), (2, 1), (1, 2), (2, 2)];
        }

        internal override Figure Rotate() => new SquareFigure();
    }
}
