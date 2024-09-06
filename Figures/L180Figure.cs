namespace Tetris.Figures
{
    public sealed class L180Figure : Figure
    {
        public L180Figure()
        {
            Coordinates = [new(1, 0), new(1, 1), new(1, 2), new(2, 2)];
        }

        protected override Figure GetRotated() => new L270Figure();
    }
}
