namespace Tetris.Figures
{
    internal class L0Figure : Figure
    {
        internal L0Figure()
        {
            Coordinates = [(1, 0), (2, 0), (2, 1), (2, 2)];
        }

        internal override Figure Rotate() => new L90Figure();
    }
}
