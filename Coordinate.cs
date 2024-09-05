namespace Tetris
{
    public struct Coordinate(int x, int y)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;

        public static Coordinate operator +(Coordinate a, Coordinate b) => new(a.X + b.X, a.Y + b.Y);
        public static Coordinate operator -(Coordinate a, Coordinate b) => new(a.X - b.X, a.Y - b.Y);
    }
}
