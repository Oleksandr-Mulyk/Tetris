namespace Tetris.Figures
{
    internal class Z0Figure : Figure
    {
        internal Z0Figure()
        {
            Coordinates = [new(0, 1), new(1, 1), new(1, 2), new(2, 2)];
        }

        internal override Figure Rotate() => new Z90Figure();
    }
}
