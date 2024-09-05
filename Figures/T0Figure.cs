namespace Tetris.Figures
{
    public class T0Figure : Figure
    {
        public T0Figure()
        {
            Coordinates = [new(0, 1), new(1, 1), new(2, 1), new(1, 2)];
        }

        protected override Figure GetRotated() => new T90Figure();
    }
}
