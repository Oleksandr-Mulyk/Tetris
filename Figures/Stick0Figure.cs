namespace Tetris.Figures
{
    internal class Stick0Figure : Figure
    {
        public Stick0Figure()
        {
            Coordinates = [(1, 0), (1, 1), (1, 2), (1, 3)];
        }

        internal override Figure Rotate() => new Stick90Figure();
    }
}
