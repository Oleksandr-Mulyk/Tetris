namespace Tetris.Figures
{
    internal class L180Figure : Figure
    {
        internal L180Figure()
        {
            Coordinates = [(1, 0), (1, 1), (1, 2), (2, 2)];
        }

        internal override Figure Rotate() => new L270Figure();
    }
}
