namespace Tetris.Game.Figures
{
    public sealed class SquareFigure : Figure
    {
        public SquareFigure()
        {
            Coordinates = [new(1, 1), new(2, 1), new(1, 2), new(2, 2)];
        }

        protected override Figure Rotated => new SquareFigure();
    }
}
