namespace Tetris.Figures
{
    public sealed class S0Figure : Figure
    {
        public S0Figure()
        {
            Coordinates = [new(1, 1), new(2, 1), new(0, 2), new(1, 2)];
        }

        protected override Figure GetRotated() => new S90Figure();
    }
}
