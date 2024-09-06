namespace Tetris.Figures
{
    public sealed class R90Figure : Figure
    {
        public R90Figure()
        {
            Coordinates = [new(0, 1), new(1, 1), new(2, 1), new(2, 2)];
        }

        protected override Figure Rotated => new R180Figure();
    }
}
