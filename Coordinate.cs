namespace Tetris
{
    internal struct Coordinate
    {
        internal int X { get; set; }
        internal int Y { get; set; }

        internal Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
