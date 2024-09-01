namespace Tetris.Figures
{
    internal class L270Figure : Figure
    {
        internal L270Figure()
        {
            Coordinates = [new(0, 1), new(1, 1), new(2, 1), new(0, 2)];
        }

        internal override Figure Rotate() => new L0Figure();
    }
}
