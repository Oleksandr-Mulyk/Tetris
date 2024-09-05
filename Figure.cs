namespace Tetris
{
    public abstract class Figure
    {
        public List<Coordinate> Coordinates { get; set; }

        public Coordinate Point { get; set; } = new(0, 0);

        public void Move(Coordinate point)
        {
            for (int i = 0; i < Coordinates.Count; i++)
            {
                Coordinates[i] = Coordinates[i] + point;
            }
            Point = Point + point;
        }

        public Figure Rotate()
        {
            Figure figure = GetRotated();
            figure.Move(Point);

            return figure;
        }

        protected abstract Figure GetRotated();
    }
}
