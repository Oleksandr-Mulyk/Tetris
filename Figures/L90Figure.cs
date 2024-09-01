namespace Tetris.Figures
{
    internal class L90Figure : Figure
    {
        internal L90Figure()
        {
            Coordinates = [new(2, 1), new(0, 2), new(1, 2), new(2, 2)];
        }

        internal override Figure Rotate() => new L180Figure();
    }
}
