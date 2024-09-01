namespace Tetris
{
    internal abstract class Figure
    {
        internal List<Coordinate> Coordinates { get; set; }

        internal void MoveLeft() => Move(new(-1, 0));

        internal void MoveRight() => Move(new(1, 0));

        internal void MoveDown() => Move(new(0, 1));

        internal void Move(Coordinate point)
        {
            for (int i = 0; i < Coordinates.Count; i++)
            {
                Coordinates[i] = new(Coordinates[i].X + point.X, Coordinates[i].Y + point.Y);
            }
        }

        internal abstract Figure Rotate();
    }
}
