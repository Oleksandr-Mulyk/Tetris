namespace Tetris.Figures
{
    internal class R270Figure : Figure
    {
        internal R270Figure()
        {
            Coordinates = [(0, 1), (0, 2), (1, 2), (2, 2)];
        }

        internal override Figure Rotate() => new R0Figure();
    }
}
