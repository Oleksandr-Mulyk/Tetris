namespace Tetris.Game.Figures
{
    public sealed class R180Figure : Figure
    {
        public R180Figure()
        {
            Coordinates = [new(2, 0), new(2, 1), new(1, 2), new(2, 2)];
        }

        protected override Figure Rotated => new R270Figure();
    }
}
