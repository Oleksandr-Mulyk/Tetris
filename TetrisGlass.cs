namespace Tetris
{
    internal class TetrisGlass : ITetrisGlass
    {
        public Size Size { get; set; }

        public bool[,] State { get; set; }

        public TetrisGlass()
        {
            Size = new Size(10, 20);
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
