﻿namespace Tetris
{
    internal class TetrisGame : ITetrisGame
    {
        internal TetrisGame()
        {
            TetrisGlass = new TetrisGlass();
            FigureSize = new(4, 4);
            FigurePoint = new(4, -2);
        }
    }
}
