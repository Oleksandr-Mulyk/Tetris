namespace Tetris.Figures
{
    internal class L180Figure : Figure
    {
        internal L180Figure()
        {
            Coordinates = [new(1, 0), new(1, 1), new(1, 2), new(2, 2)];
        }

        internal override Figure Rotate() => new L270Figure();
    }
}
