namespace Tetris.Figures
{
    internal class Stick0Figure : Figure
    {
        public Stick0Figure()
        {
            Coordinates = [new(1, 0), new(1, 1), new(1, 2), new(1, 3)];
        }

        internal override Figure Rotate() => new Stick90Figure();
    }
}
