namespace Tetris.Figures
{
    public class Stick90Figure : Figure
    {
        public Stick90Figure()
        {
            Coordinates = [new(0, 1), new(1, 1), new(2, 1), new(3, 1)];
        }

        protected override Figure GetRotated() => new Stick0Figure();
    }
}
