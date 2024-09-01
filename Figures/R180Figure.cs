namespace Tetris.Figures
{
    internal class R180Figure : Figure
    {
        internal R180Figure()
        {
            Coordinates = [new(2, 0), new(2, 1), new(1, 2), new(2, 2)];
        }

        internal override Figure Rotate() => new R270Figure();
    }
}
