namespace Tetris.Figures
{
    internal class L0Figure : Figure
    {
        internal L0Figure()
        {
            Coordinates = [new(1, 0), new(2, 0), new(2, 1), new(2, 2)];
        }

        internal override Figure Rotate() => new L90Figure();
    }
}
