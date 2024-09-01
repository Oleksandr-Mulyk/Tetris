namespace Tetris.Figures
{
    internal class R90Figure : Figure
    {
        internal R90Figure()
        {
            Coordinates = [new(0, 1), new(1, 1), new(2, 1), new(2, 2)];
        }

        internal override Figure Rotate() => new R180Figure();
    }
}
