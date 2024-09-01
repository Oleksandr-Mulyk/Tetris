namespace Tetris.Figures
{
    internal class R90Figure : Figure
    {
        internal R90Figure()
        {
            Coordinates = [(0, 1), (1, 1), (2, 1), (2, 2)];
        }

        internal override Figure Rotate() => new R180Figure();
    }
}
