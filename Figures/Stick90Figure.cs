namespace Tetris.Figures
{
    internal class Stick90Figure : Figure
    {
        public Stick90Figure()
        {
            Coordinates = [new(0, 1), new(1, 1), new(2, 1), new(3, 1)];
        }

        internal override Figure Rotate() => new Stick0Figure();
    }
}
