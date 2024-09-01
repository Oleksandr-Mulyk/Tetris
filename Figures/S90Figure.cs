namespace Tetris.Figures
{
    internal class S90Figure : Figure
    {
        internal S90Figure()
        {
            Coordinates = [new(1, 0), new(1, 1), new(2, 1), new(2, 2)];
        }

        internal override Figure Rotate()=>new S0Figure();
    }
}
