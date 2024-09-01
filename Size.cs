namespace Tetris
{
    internal struct Size
    {
        internal int Width { get; set; }

        internal int Height { get; set; }

        internal Size(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
