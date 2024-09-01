namespace Tetris.Figures
{
    internal class L270Figure : Figure
    {
        internal L270Figure()
        {
            Coordinates = [(0, 1), (1, 1), (2, 1), (0, 2)];
        }

        internal override Figure Rotate() => new L0Figure();
    }
}
