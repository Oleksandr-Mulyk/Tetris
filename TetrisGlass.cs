namespace Tetris
{
    public class TetrisGlass : ITetrisGlass
    {
        public Size Size { get; set; } = new Size(10, 20);

        public bool[,] State { get; set; }

        public TetrisGlass()
        {
            State = new bool[Size.Width, Size.Height];
        }

        public bool this[int X, int Y]
        {
            get
            {
                return State[X, Y];
            }
            set
            {
                State[X, Y] = value;
            }
        }
    }
}
