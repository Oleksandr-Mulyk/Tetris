namespace Tetris.Figures
{
    internal class R0Figure : Figure
    {
        internal R0Figure()
        {
            Coordinates = [new(1, 0), new(2, 0), new(1, 1), new(1, 2)];
        }

        internal override Figure Rotate() => new R90Figure();
    }
}
