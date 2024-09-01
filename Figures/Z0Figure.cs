namespace Tetris.Figures
{
    internal class Z0Figure : Figure
    {
        internal Z0Figure()
        {
            Coordinates = [(0, 1), (1, 1), (1, 2), (2, 2)];
        }

        internal override Figure Rotate() => new Z90Figure();
    }
}
