namespace Tetris.Game.Figures
{
    public sealed class Stick0Figure : Figure
    {
        public Stick0Figure()
        {
            Coordinates = [new(1, 0), new(1, 1), new(1, 2), new(1, 3)];
        }

        protected override Figure Rotated => new Stick90Figure();
    }
}
