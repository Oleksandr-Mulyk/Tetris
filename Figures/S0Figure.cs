namespace Tetris.Figures
{
    internal class S0Figure : Figure
    {
        public S0Figure()
        {
            Coordinates = [(1, 1), (2, 1), (0, 2), (1, 2)];
        }

        internal override Figure Rotate() => new S90Figure();
    }
}
