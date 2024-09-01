namespace Tetris.Figures
{
    internal class Stick90Figure : Figure
    {
        public Stick90Figure()
        {
            Coordinates = [(0, 1), (1, 1), (2, 1), (3, 1)];
        }

        internal override Figure Rotate() => new Stick0Figure();
    }
}
