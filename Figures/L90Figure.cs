namespace Tetris.Figures
{
    internal class L90Figure : Figure
    {
        internal L90Figure()
        {
            Coordinates = [(2, 1), (0, 2), (1, 2), (2, 2)];
        }

        internal override Figure Rotate() => new L180Figure();
    }
}
