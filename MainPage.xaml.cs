using Tetris.Figures;

using MauiColor = Microsoft.Maui.Graphics.Color;
using Coordinate = (int X, int Y);
using GridSize = (int ColumnCount, int RowCount);

namespace Tetris
{
    public partial class MainPage : ContentPage
    {
        private readonly (int Width, int Height) _glassSize = (10, 20);

        private readonly (int Width, int Height) _nextSize = (4, 4);

        private bool[,] _glassCellFilled;

        private readonly IDispatcherTimer _mainTimer;

        private readonly IDispatcherTimer _actionTimer;

        private Figure? _currentFigure;

        private Figure? _nextFigure;

        private Coordinate _figurePoint;

        private int _score = 0;

        private int _level = 1;

        public MainPage()
        {
            InitializeComponent();

            _glassCellFilled = new bool[_glassSize.Width, _glassSize.Height];

            InitializeGlass(GridGlass, _glassSize.Width, _glassSize.Height);
            InitializeGlass(GridNext, _nextSize.Width, _nextSize.Height);

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
            _glassCellFilled = new bool[_glassSize.Width, _glassSize.Height];

            Border[,] borders = GetBorderList(GridGlass);
            for(int i = 0; i < _glassSize.Width; i++)
            {
                for (int j = 0;  j < _glassSize.Height; j++)
                {
                    borders[i, j].Background = MauiColor.FromArgb("#919191");
                    borders[i, j].Stroke = MauiColor.FromArgb("#6E6E6E");
                }
            }

            Border[,] bordersNext = GetBorderList(GridNext);
            for (int i = 0; i < _nextSize.Width; i++)
            {
                for (int j = 0; j < _nextSize.Height; j++)
                {
                    bordersNext[i, j].Background = MauiColor.FromArgb("#919191");
                    bordersNext[i, j].Stroke = MauiColor.FromArgb("#6E6E6E");
                }
            }

            _score = 0;
            _nextFigure = null;
            _currentFigure = null;
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
            bool nextFigureNeded = _nextFigure == null || _currentFigure == null;

            if (_currentFigure == null)
            {
                _currentFigure = _nextFigure ?? FigureGenerator.GetRandomFigure();
                _figurePoint = (3, _currentFigure is Stick0Figure ? -3 : -2);
                _currentFigure!.Move(_figurePoint);
            }

            if (nextFigureNeded)
            {
                if (_nextFigure != null)
                {
                    ClearFigure(GridNext, _nextFigure);
                }
                _nextFigure = FigureGenerator.GetRandomFigure();
                DrawFigure(GridNext, _nextFigure);
            }

            _mainTimer.Start();
        }

        private void MoveLeft()
        {
            List<Coordinate> newCoordinates = _currentFigure!.Coordinates.Select(coordinate => (--coordinate.X, coordinate.Y)).ToList();
            if (IsMovingValid(newCoordinates))
            {
                ClearFigure(GridGlass, _currentFigure);
                _currentFigure.Coordinates = newCoordinates;
                DrawFigure(GridGlass, _currentFigure);
                _figurePoint.X--;
            }
        }

        private void MoveRight()
        {
            List<Coordinate> newCoordinates = _currentFigure!.Coordinates.Select(coordinate => (++coordinate.X, coordinate.Y)).ToList();
            if (IsMovingValid(newCoordinates))
            {
                ClearFigure(GridGlass, _currentFigure);
                _currentFigure.Coordinates = newCoordinates;
                DrawFigure(GridGlass, _currentFigure);
                _figurePoint.X++;
            }
        }

        private void MoveDown()
        {
            List<Coordinate> newCoordinates = _currentFigure!.Coordinates.Select(coordinate => (coordinate.X, ++coordinate.Y)).ToList();
            if (IsMovingValid(newCoordinates))
            {
                ClearFigure(GridGlass, _currentFigure);
                _currentFigure.Coordinates = newCoordinates;
                DrawFigure(GridGlass, _currentFigure);
                _figurePoint.Y++;
            }
            else
            {
                bool gameIsOver = false;
                foreach (Coordinate coordinate in _currentFigure!.Coordinates)
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
            for (int i = 0; i < _glassSize.Height; i++)
            {
                bool lineIsFull = true;
                for (int j = 0;  j < _glassSize.Width; j++)
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
                        for (int j = 0; j < _glassSize.Width; j++)
                        {
                            _glassCellFilled[j, i] = i != 0 && _glassCellFilled[j, i - 1];
                        }
                    }
                }

                Border[,] borders = GetBorderList(GridGlass);
                for (int i = 0; i < _glassSize.Width; i++)
                {
                    for (int j = 0; j < _glassSize.Height; j++)
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
            foreach (Coordinate coordinate in _currentFigure!.Coordinates)
            {
                _glassCellFilled[coordinate.X, coordinate.Y] = true;
            }

            ClearFigure(GridNext, _nextFigure!);
            _currentFigure = _nextFigure;
            _figurePoint = (3, _currentFigure is Stick0Figure ? -3 : -2);
            _currentFigure!.Move(_figurePoint);
            _nextFigure = FigureGenerator.GetRandomFigure();
            DrawFigure(GridNext, _nextFigure);
        }

        private void Gameover()
        {
            _mainTimer?.Stop();
            LabelGameover.Text = "GAME OVER";
        }

        private void Rotate()
        {
            Figure rotatedFigure = _currentFigure!.Rotate();
            rotatedFigure.Move(_figurePoint);

            if (IsMovingValid(rotatedFigure.Coordinates))
            {
                ClearFigure(GridGlass, _currentFigure);
                _currentFigure = rotatedFigure;
                DrawFigure(GridGlass, _currentFigure);
            }
        }

        private bool IsMovingValid(List<Coordinate> coordinates)
        {
            foreach (Coordinate coordinate in coordinates)
            {
                if (coordinate.X < 0 || coordinate.X >= _glassSize.Width || coordinate.Y >= _glassSize.Height
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
            GridSize gridSize = GetGridSize(grid);

            foreach (Coordinate coordinate in figure.Coordinates)
            {
                if (coordinate.X >= 0 && coordinate.Y >= 0 && coordinate.X < gridSize.ColumnCount && coordinate.Y < gridSize.RowCount)
                {
                    borders[coordinate.X, coordinate.Y].Background = MauiColor.FromArgb("#919191");
                    borders[coordinate.X, coordinate.Y].Stroke = MauiColor.FromArgb("#6E6E6E");
                }
            }
        }

        private static void DrawFigure(Grid grid, Figure figure)
        {
            Border[,] borders = GetBorderList(grid);
            GridSize gridSize = GetGridSize(grid);

            foreach (Coordinate coordinate in figure.Coordinates)
            {
                if (coordinate.X >= 0 && coordinate.Y >= 0 && coordinate.X < gridSize.ColumnCount && coordinate.Y < gridSize.RowCount)
                {
                    borders[coordinate.X, coordinate.Y].Background = MauiColor.FromArgb("#141414");
                    borders[coordinate.X, coordinate.Y].Stroke = MauiColor.FromArgb("#141414");
                }
            }
        }

        private static Border[,] GetBorderList(Grid grid)
        {
            Border[] borders = grid.Children.Where(child => child is Border).Select(child => (Border)child).ToArray();
            GridSize gridSize = GetGridSize(grid);

            Border[,] result = new Border[gridSize.ColumnCount, gridSize.RowCount];

            for (int i = 0; i < gridSize.ColumnCount; i++)
            {
                for (int j = 0; j < gridSize.RowCount; j++)
                {
                    result[i, j] = borders[i * gridSize.RowCount + j];
                }
            }

            return result;
        }

        private static GridSize GetGridSize(Grid grid) => (grid.ColumnDefinitions.Count, grid.RowDefinitions.Count);
    }
}
