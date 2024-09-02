namespace Tetris
{
    internal interface ITetrisGlass
    {
        public Size Size { get; set; }

        public bool[,] State { get; set; }

        public bool this[int X, int Y] { get; set; }
    }
}
