namespace Tetris.Figures
{
    internal class SquareFigure : Figure
    {
        internal SquareFigure()
        {
            Coordinates = [new(1,1), new(2, 1), new(1, 2), new(2, 2)];
        }

        internal override Figure Rotate() => new SquareFigure();
    }
}
