namespace Tetris.Game.Figures
{
    public sealed class Z90Figure : Figure
    {
        public Z90Figure()
        {
            Coordinates = [new(2, 0), new(1, 1), new(2, 1), new(1, 2)];
        }

        protected override Figure Rotated => new Z0Figure();
    }
}
