namespace Tetris.Game.Figures
{
    public sealed class T180Figure : Figure
    {
        public T180Figure()
        {
            Coordinates = [new(1, 1), new(0, 2), new(1, 2), new(2, 2)];
        }

        protected override Figure Rotated => new T270Figure();
    }
}
