namespace Tetris.Figures
{
    internal class T0Figure : Figure
    {
        internal T0Figure()
        {
            Coordinates = [new(0, 1), new(1, 1), new(2, 1), new(1, 2)];
        }

        internal override Figure Rotate() => new T90Figure();
    }
}
