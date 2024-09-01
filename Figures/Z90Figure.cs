namespace Tetris.Figures
{
    internal class Z90Figure : Figure
    {
        internal Z90Figure()
        {
            Coordinates = [(2, 0), (1, 1), (2, 1), (1, 2)];
        }

        internal override Figure Rotate() => new Z0Figure();
    }
}
