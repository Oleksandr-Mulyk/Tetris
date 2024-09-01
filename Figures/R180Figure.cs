namespace Tetris.Figures
{
    internal class R180Figure : Figure
    {
        internal R180Figure()
        {
            Coordinates = [(2, 0), (2, 1), (1, 2), (2, 2)];
        }

        internal override Figure Rotate() => new R270Figure();
    }
}
