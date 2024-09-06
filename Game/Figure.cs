namespace Tetris.Game
{
    public abstract class Figure
    {
        public List<Coordinate> Coordinates { get; set; }

        public Coordinate Point { get; set; } = new(0, 0);

        public void Move(Coordinate point)
        {
            Coordinates = Coordinates.Select(coordinate => coordinate + point).ToList();
            Point += point;
        }

        public void Move(int X, int Y) => Move(new Coordinate(X, Y));

        public Figure Rotate()
        {
            Figure figure = Rotated;
            figure.Move(Point);

            return figure;
        }

        protected abstract Figure Rotated { get; }
    }
}
