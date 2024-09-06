namespace Tetris.Game.Figures
{
    public sealed class L0Figure : Figure
    {
        public L0Figure()
        {
            Coordinates = [new(1, 0), new(2, 0), new(2, 1), new(2, 2)];
        }

        protected override Figure Rotated => new L90Figure();
    }
}
