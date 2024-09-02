using Tetris.Figures;

using MauiColor = Microsoft.Maui.Graphics.Color;

namespace Tetris
{
    public partial class MainPage : ContentPage
    {
        ITetrisGame _game;

        private bool[,] _glassCellFilled;

        private readonly IDispatcherTimer _mainTimer;

        private readonly IDispatcherTimer _actionTimer;

        private int _score = 0;

        private int _level = 1;

        public MainPage()
        {
            InitializeComponent();

            _game = new TetrisGame();

            _glassCellFilled = new bool[_game.TetrisGlass.Size.Width, _game.TetrisGlass.Size.Height];

            InitializeGlass(GridGlass, _game.TetrisGlass.Size.Width, _game.TetrisGlass.Size.Height);
            InitializeGlass(GridNext, _game.FigureSize.Width, _game.FigureSize.Height);

            _mainTimer = Dispatcher.CreateTimer();
            _mainTimer.Interval = TimeSpan.FromSeconds(1);
            _mainTimer.Tick += (s, e) => MoveDown();

            ButtonPause.Pressed += (_, _) => _mainTimer.Stop();
            ButtonStart.Pressed += (_, _) => GameStart();
            ButtonReset.Pressed += ButtonReset_Pressed;

            ButtonLeft.Pressed += (sender, _) => Button_Press((Button)sender, MoveLeft);
            ButtonRight.Pressed += (sender, _) => Button_Press((Button)sender, MoveRight);
            ButtonDown.Pressed += (sender, _) => Button_Press((Button)sender, MoveDown);
            ButtonRotate.Pressed += (_, _) => Rotate();

            _actionTimer = Dispatcher.CreateTimer();
            _actionTimer.Interval = TimeSpan.FromSeconds(0.1);
        }

        private void ButtonReset_Pressed(object? sender, EventArgs e)
        {
            _mainTimer?.Stop();
            _glassCellFilled = new bool[_game.TetrisGlass.Size.Width, _game.TetrisGlass.Size.Height];

            Border[,] borders = GetBorderList(GridGlass);
            for(int i = 0; i < _game.TetrisGlass.Size.Width; i++)
            {
                for (int j = 0;  j < _game.TetrisGlass.Size.Height; j++)
                {
                    borders[i, j].Background = MauiColor.FromArgb("#919191");
                    borders[i, j].Stroke = MauiColor.FromArgb("#6E6E6E");
                }
            }

            Border[,] bordersNext = GetBorderList(GridNext);
            for (int i = 0; i < _game.FigureSize.Width; i++)
            {
                for (int j = 0; j < _game.FigureSize.Height; j++)
                {
                    bordersNext[i, j].Background = MauiColor.FromArgb("#919191");
                    bordersNext[i, j].Stroke = MauiColor.FromArgb("#6E6E6E");
                }
            }

            _score = 0;
            _game.NextFigure = null;
            _game.CurrentFigure = null;
            LabelScore.Text = _score.ToString("000000");
            LabelGameover.Text = string.Empty;
        }

        private void Button_Press(Button button, Action action)
        {
            action();
            _actionTimer.Tick += actionTimer_Tick;
            _actionTimer.Start();

            void actionTimer_Tick(object? sender, EventArgs e)
            {
                if (button.IsPressed)
                {
                    action();
                }

                else
                {
                    _actionTimer.Stop();
                    _actionTimer.Tick -= actionTimer_Tick;
                }
            }
        }

        private static void InitializeGlass(Grid grid,int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                RowDefinition rowDefinition = new();
                grid.RowDefinitions.Add(rowDefinition);
            }

            for (int i = 0; i < width; i++)
            {
                ColumnDefinition columnDefinition = new();
                grid.ColumnDefinitions.Add(columnDefinition);
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Border border = new()
                    {
                        Stroke = MauiColor.FromArgb("#6E6E6E"),
                        StrokeThickness = 1,
                        Margin = 1,
                        HeightRequest = 20,
                        WidthRequest = 20,
                    };
                    grid.Add(border, i, j);
                }
            }
        }

        private void GameStart()
        {
            bool nextFigureNeded = _game.NextFigure == null || _game.CurrentFigure == null;

            if (_game.CurrentFigure == null)
            {
                _game.CurrentFigure = _game.NextFigure ?? FigureGenerator.GetRandomFigure();
                _game.FigurePoint = new(3, _game.CurrentFigure is Stick0Figure ? -3 : -2);
                _game.CurrentFigure!.Move(_game.FigurePoint);
            }

            if (nextFigureNeded)
            {
                if (_game.NextFigure != null)
                {
                    ClearFigure(GridNext, _game.NextFigure);
                }
                _game.NextFigure = FigureGenerator.GetRandomFigure();
                DrawFigure(GridNext, _game.NextFigure);
            }

            _mainTimer.Start();
        }

        private void MoveLeft()
        {
            List<Coordinate> newCoordinates = _game.CurrentFigure!.Coordinates.Select(coordinate => new Coordinate(--coordinate.X, coordinate.Y)).ToList();
            if (IsMovingValid(newCoordinates))
            {
                ClearFigure(GridGlass, _game.CurrentFigure);
                _game.CurrentFigure.Coordinates = newCoordinates;
                DrawFigure(GridGlass, _game.CurrentFigure);
                _game.FigurePoint = new(_game.FigurePoint.X - 1, _game.FigurePoint.Y);
            }
        }

        private void MoveRight()
        {
            List<Coordinate> newCoordinates = _game.CurrentFigure!.Coordinates.Select(coordinate => new Coordinate(++coordinate.X, coordinate.Y)).ToList();
            if (IsMovingValid(newCoordinates))
            {
                ClearFigure(GridGlass, _game.CurrentFigure);
                _game.CurrentFigure.Coordinates = newCoordinates;
                DrawFigure(GridGlass, _game.CurrentFigure);
               _game.FigurePoint = new(_game.FigurePoint.X + 1, _game.FigurePoint.Y);
            }
        }

        private void MoveDown()
        {
            List<Coordinate> newCoordinates = _game.CurrentFigure!.Coordinates.Select(coordinate => new Coordinate(coordinate.X, ++coordinate.Y)).ToList();
            if (IsMovingValid(newCoordinates))
            {
                ClearFigure(GridGlass, _game.CurrentFigure);
                _game.CurrentFigure.Coordinates = newCoordinates;
                DrawFigure(GridGlass, _game.CurrentFigure);
                _game.FigurePoint = new(_game.FigurePoint.X, _game.FigurePoint.Y + 1);
            }
            else
            {
                bool gameIsOver = false;
                foreach (Coordinate coordinate in _game.CurrentFigure!.Coordinates)
                {
                    if (coordinate.Y < 0)
                    {
                        Gameover();
                        gameIsOver = true;
                    }
                }

                if (!gameIsOver)
                {
                    NextFigure();
                    CheckLines();
                }
            }
        }

        private void CheckLines()
        {
            List<int> fullLines = new();
            for (int i = 0; i < _game.TetrisGlass.Size.Height; i++)
            {
                bool lineIsFull = true;
                for (int j = 0;  j < _game.TetrisGlass.Size.Width; j++)
                {
                    lineIsFull &= _glassCellFilled[j, i];
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
                        for (int j = 0; j < _game.TetrisGlass.Size.Width; j++)
                        {
                            _glassCellFilled[j, i] = i != 0 && _glassCellFilled[j, i - 1];
                        }
                    }
                }

                Border[,] borders = GetBorderList(GridGlass);
                for (int i = 0; i < _game.TetrisGlass.Size.Width; i++)
                {
                    for (int j = 0; j < _game.TetrisGlass.Size.Height; j++)
                    {
                        borders[i, j].Background = _glassCellFilled[i, j] ? MauiColor.FromArgb("#141414") : MauiColor.FromArgb("#919191");
                        borders[i, j].Stroke = _glassCellFilled[i, j] ? MauiColor.FromArgb("#141414") : MauiColor.FromArgb("#6E6E6E");
                    }
                }

                _score += fullLines.Count switch
                {
                    1 => 100,
                    2 => 300,
                    3 => 500,
                    4 => 800,
                    _ => 0
                };

                LabelScore.Text = _score.ToString("000000");

                if (_level < 10 && _score >= (_level) * 10000)
                {
                    ++_level;
                    _mainTimer.Stop();
                    _mainTimer.Interval = TimeSpan.FromSeconds(1 + 0.1 - _level / 10.0);
                    _mainTimer.Start();
                    LabelLevel.Text = "Level: " + _level;
                }
            }
        }

        private void NextFigure()
        {
            foreach (Coordinate coordinate in _game.CurrentFigure!.Coordinates)
            {
                _glassCellFilled[coordinate.X, coordinate.Y] = true;
            }

            ClearFigure(GridNext, _game.NextFigure!);
            _game.CurrentFigure = _game.NextFigure;
            _game.FigurePoint = new(3, _game.CurrentFigure is Stick0Figure ? -3 : -2);
            _game.CurrentFigure!.Move(_game.FigurePoint);
            _game.NextFigure = FigureGenerator.GetRandomFigure();
            DrawFigure(GridNext, _game.NextFigure);
        }

        private void Gameover()
        {
            _mainTimer?.Stop();
            LabelGameover.Text = "GAME OVER";
        }

        private void Rotate()
        {
            Figure rotatedFigure = _game.CurrentFigure!.Rotate();
            rotatedFigure.Move(_game.FigurePoint);

            if (IsMovingValid(rotatedFigure.Coordinates))
            {
                ClearFigure(GridGlass, _game.CurrentFigure);
                _game.CurrentFigure = rotatedFigure;
                DrawFigure(GridGlass, _game.CurrentFigure);
            }
        }

        private bool IsMovingValid(List<Coordinate> coordinates)
        {
            foreach (Coordinate coordinate in coordinates)
            {
                if (coordinate.X < 0 || coordinate.X >= _game.TetrisGlass.Size.Width || coordinate.Y >= _game.TetrisGlass.Size.Height
                    || (coordinate.Y >= 0 && _glassCellFilled[coordinate.X, coordinate.Y]))
                {
                    return false;
                }
            }

            return true;
        }

        private static void ClearFigure(Grid grid, Figure figure)
        {
            Border[,] borders = GetBorderList(grid);
            Size gridSize = GetGridSize(grid);

            foreach (Coordinate coordinate in figure.Coordinates)
            {
                if (coordinate.X >= 0 && coordinate.Y >= 0 && coordinate.X < gridSize.Width && coordinate.Y < gridSize.Height)
                {
                    borders[coordinate.X, coordinate.Y].Background = MauiColor.FromArgb("#919191");
                    borders[coordinate.X, coordinate.Y].Stroke = MauiColor.FromArgb("#6E6E6E");
                }
            }
        }

        private static void DrawFigure(Grid grid, Figure figure)
        {
            Border[,] borders = GetBorderList(grid);
            Size gridSize = GetGridSize(grid);

            foreach (Coordinate coordinate in figure.Coordinates)
            {
                if (coordinate.X >= 0 && coordinate.Y >= 0 && coordinate.X < gridSize.Width && coordinate.Y < gridSize.Height)
                {
                    borders[coordinate.X, coordinate.Y].Background = MauiColor.FromArgb("#141414");
                    borders[coordinate.X, coordinate.Y].Stroke = MauiColor.FromArgb("#141414");
                }
            }
        }

        private static Border[,] GetBorderList(Grid grid)
        {
            Border[] borders = grid.Children.Where(child => child is Border).Select(child => (Border)child).ToArray();
            Size gridSize = GetGridSize(grid);

            Border[,] result = new Border[gridSize.Width, gridSize.Height];

            for (int i = 0; i < gridSize.Width; i++)
            {
                for (int j = 0; j < gridSize.Height; j++)
                {
                    result[i, j] = borders[i * gridSize.Height + j];
                }
            }

            return result;
        }

        private static Size GetGridSize(Grid grid) => new(grid.ColumnDefinitions.Count, grid.RowDefinitions.Count);
    }
}
