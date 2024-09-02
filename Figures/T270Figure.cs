namespace Tetris.Figures
{
    internal class T270Figure : Figure
    {
        internal T270Figure()
        {
            Coordinates = [new(1, 0), new(1, 1), new(2, 1), new(1, 2)];
        }

        internal override Figure Rotate() => new T0Figure();
    }
}
