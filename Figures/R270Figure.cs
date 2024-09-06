﻿namespace Tetris.Figures
{
    public sealed class R270Figure : Figure
    {
        public R270Figure()
        {
            Coordinates = [new(0, 1), new(0, 2), new(1, 2), new(2, 2)];
        }

        protected override Figure GetRotated() => new R0Figure();
    }
}
