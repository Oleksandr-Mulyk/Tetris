namespace Tetris.Figures
{
    public sealed class R0Figure : Figure
    {
        public R0Figure()
        {
            Coordinates = [new(1, 0), new(2, 0), new(1, 1), new(1, 2)];
        }

        protected override Figure GetRotated() => new R90Figure();
    }
}
