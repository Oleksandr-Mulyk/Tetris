namespace Tetris.Figures
{
    internal class T90Figure : Figure
    {
        internal T90Figure()
        {
            Coordinates = [new(2, 0), new(1, 1), new(2, 1), new(2, 2)];
        }

        internal override Figure Rotate() => new T180Figure();
    }
}
