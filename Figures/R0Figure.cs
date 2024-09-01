namespace Tetris.Figures
{
    internal class R0Figure : Figure
    {
        internal R0Figure()
        {
            Coordinates = [(1, 0), (2, 0), (1, 1), (1, 2)];
        }

        internal override Figure Rotate() => new R90Figure();
    }
}
