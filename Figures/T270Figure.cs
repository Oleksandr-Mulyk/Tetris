namespace Tetris.Figures
{
    public class T270Figure : Figure
    {
        public T270Figure()
        {
            Coordinates = [new(1, 0), new(1, 1), new(2, 1), new(1, 2)];
        }

        protected override Figure GetRotated() => new T0Figure();
    }
}
