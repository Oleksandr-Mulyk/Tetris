namespace Tetris.Figures
{
    internal class Z90Figure : Figure
    {
        internal Z90Figure()
        {
            Coordinates = [new(2, 0), new(1, 1), new(2, 1), new(1, 2)];
        }

        internal override Figure Rotate() => new Z0Figure();
    }
}
