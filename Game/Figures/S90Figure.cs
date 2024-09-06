namespace Tetris.Game.Figures
{
    public sealed class S90Figure : Figure
    {
        public S90Figure()
        {
            Coordinates = [new(1, 0), new(1, 1), new(2, 1), new(2, 2)];
        }

        protected override Figure Rotated => new S0Figure();
    }
}
