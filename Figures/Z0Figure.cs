namespace Tetris.Figures
{
    public sealed class Z0Figure : Figure
    {
        public Z0Figure()
        {
            Coordinates = [new(0, 1), new(1, 1), new(1, 2), new(2, 2)];
        }

        protected override Figure Rotated => new Z90Figure();
    }
}
