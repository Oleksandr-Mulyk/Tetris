namespace Tetris.Figures
{
    public sealed class T90Figure : Figure
    {
        public T90Figure()
        {
            Coordinates = [new(2, 0), new(1, 1), new(2, 1), new(2, 2)];
        }

        protected override Figure Rotated => new T180Figure();
    }
}
