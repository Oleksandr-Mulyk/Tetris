using Tetris.Figures;

namespace Tetris
{
    internal static class FigureGenerator
    {
        internal static Figure GetRandomFigure()
        {
            Random rnd = new();
            int figureIndex = rnd.Next(0, 14);

            return figureIndex switch
            {
                0 => new SquareFigure(),
                1 => new Stick0Figure(),
                2 => new Stick90Figure(),
                3 => new Z0Figure(),
                4 => new Z90Figure(),
                5 => new S0Figure(),
                6 => new S90Figure(),
                7 => new L0Figure(),
                8 => new L90Figure(),
                9 => new L180Figure(),
                10 => new L270Figure(),
                11 => new R0Figure(),
                12 => new R90Figure(),
                13 => new R180Figure(),
                14 => new R270Figure(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
