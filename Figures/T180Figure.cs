namespace Tetris.Figures
{
    internal class T180Figure : Figure
    {
        internal T180Figure()
        {
            Coordinates = [new(1, 1), new(0, 2), new(1, 2), new(2, 2)];
        }

        internal override Figure Rotate() => new T270Figure();
    }
}
