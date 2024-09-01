namespace Tetris.Figures
{
    internal class S90Figure : Figure
    {
        internal S90Figure()
        {
            Coordinates = [(1, 0), (1, 1), (2, 1), (2, 2)];
        }

        internal override Figure Rotate()=>new S0Figure();
    }
}
