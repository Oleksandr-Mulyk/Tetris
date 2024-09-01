namespace Tetris.Figures
{
    internal class R270Figure : Figure
    {
        internal R270Figure()
        {
            Coordinates = [new(0, 1), new(0, 2), new(1, 2), new(2, 2)];
        }

        internal override Figure Rotate() => new R0Figure();
    }
}
