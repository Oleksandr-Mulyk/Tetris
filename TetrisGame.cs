using Tetris.Figures;

namespace Tetris
{
    public class TetrisGame : ITetrisGame
    {
        public event FigureMovedHandler? FigureMoved;

        public event GameOverHandler? GameOver;

        public event FigureChangedHandler? FigureChanged;

        public event ScoreChangedHandler? ScoreChanged;

        public event LevelChangedHandler? LevelChanged;

        public event GlassLinesRemovedHandler? GlassLinesRemoved;

        public ITetrisGlass TetrisGlass { get; set; }

        public Size FigureSize { get; set; } = new(4, 4);

        public Figure CurrentFigure { get; set; }

        public Figure NextFigure { get; set; }

        private int _score;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                ScoreChanged?.Invoke();
            }
        }

        private int _level;

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                LevelChanged?.Invoke();
            }
        }

        public TetrisGame() => CreateNewGame();

        public void CreateNewGame()
        {
            TetrisGlass = new TetrisGlass();
            CurrentFigure = FigureGenerator.GetRandomFigure();
            CurrentFigure.Move(3, CurrentFigure is Stick0Figure ? -3 : -2);
            NextFigure = FigureGenerator.GetRandomFigure();
            Score = 0;
            Level = 0;
        }

        public void MoveLeft() => Move(-1, 0);

        public void MoveRight() => Move(1, 0);

        public void MoveDown()
        {
            if (!Move(0, 1))
            {
                bool gameIsOver = false;
                foreach (Coordinate coordinate in CurrentFigure.Coordinates)
                {
                    if (coordinate.Y < 0)
                    {
                        GameOver?.Invoke();
                        gameIsOver = true;
                    }
                    else
                    {
                        TetrisGlass[coordinate.X, coordinate.Y] = true;
                    }
                }

                if (!gameIsOver)
                {
                    ChangeFigure();
                    CheckLines();
                }
            }
        }

        private bool Move(int X, int Y)
        {
            List<Coordinate> newCoordinates = CurrentFigure.Coordinates.Select(coordinate => coordinate + new Coordinate(X, Y)).ToList();
            if (IsMovingValid(newCoordinates))
            {
                List<Coordinate> oldCoordinates = new(CurrentFigure.Coordinates);
                CurrentFigure.Move(X, Y);
                FigureMoved?.Invoke(oldCoordinates);
                return true;
            }

            return false;
        }

        private bool IsMovingValid(List<Coordinate> coordinates) =>
            coordinates?.Count(coordinate =>
            coordinate.X < 0
            || coordinate.X >= TetrisGlass.Size.Width
            || coordinate.Y >= TetrisGlass.Size.Height
            || (coordinate.Y >= 0 && TetrisGlass[coordinate.X, coordinate.Y])
            ) == 0;

        private void ChangeFigure()
        {
            CurrentFigure = NextFigure;
            CurrentFigure!.Move(3, CurrentFigure is Stick0Figure ? -3 : -2);
            NextFigure = FigureGenerator.GetRandomFigure();
            FigureChanged?.Invoke();
        }

        public void Rotate()
        {
            Figure rotatedFigure = CurrentFigure!.Rotate();

            if (IsMovingValid(rotatedFigure.Coordinates))
            {
                List<Coordinate> oldCoordinates = new(CurrentFigure.Coordinates);
                CurrentFigure = rotatedFigure;
                FigureMoved?.Invoke(oldCoordinates);
            }
        }

        private void CheckLines()
        {
            List<int> fullLines = [];
            for (int i = 0; i < TetrisGlass.Size.Height; i++)
            {
                bool lineIsFull = true;
                for (int j = 0; j < TetrisGlass.Size.Width; j++)
                {
                    lineIsFull &= TetrisGlass[j, i];
                }
                if (lineIsFull)
                {
                    fullLines.Add(i);
                }
            }

            if (fullLines.Count > 0)
            {
                foreach (int line in fullLines)
                {
                    for (int i = line; i >= 0; i--)
                    {
                        for (int j = 0; j < TetrisGlass.Size.Width; j++)
                        {
                            TetrisGlass[j, i] = i != 0 && TetrisGlass[j, i - 1];
                        }
                    }
                }

                GlassLinesRemoved?.Invoke();

                Score += fullLines.Count switch
                {
                    1 => 100,
                    2 => 300,
                    3 => 500,
                    4 => 800,
                    _ => 0
                };

                if (Level < 10 && Score >= (Level) * 10000)
                {
                    ++Level;
                }
            }
        }
    }
}
