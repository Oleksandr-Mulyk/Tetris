namespace Tetris.Game.Figures
{
    public sealed class L270Figure : Figure
    {
        public L270Figure()
        {
            Coordinates = [new(0, 1), new(1, 1), new(2, 1), new(0, 2)];
        }

        protected override Figure Rotated => new L0Figure();
    }
}
