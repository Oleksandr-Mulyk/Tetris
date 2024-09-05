using MauiColor = Microsoft.Maui.Graphics.Color;

namespace Tetris
{
    public partial class MainPage : ContentPage
    {
        private readonly ITetrisGame _game;

        private IDispatcherTimer? _mainTimer;

        private IDispatcherTimer? _actionTimer;

        private bool _newGame = true;

        public MainPage()
        {
            InitializeComponent();

            _game = new TetrisGame();

            InitializeGlass(GridGlass, _game.TetrisGlass.Size.Width, _game.TetrisGlass.Size.Height);
            InitializeGlass(GridNext, _game.FigureSize.Width, _game.FigureSize.Height);
            CreateTimers();

            HandleButtons();
            HandleGameEvents();
        }

        private void CreateTimers()
        {
            _mainTimer = Dispatcher.CreateTimer();
            _mainTimer.Interval = TimeSpan.FromSeconds(1);
            _mainTimer.Tick += (s, e) => _game.MoveDown();

            _actionTimer = Dispatcher.CreateTimer();
            _actionTimer.Interval = TimeSpan.FromSeconds(0.1);
        }

        private void HandleButtons()
        {
            ButtonPause.Pressed += (_, _) => _mainTimer?.Stop();
            ButtonStart.Pressed += (_, _) => GameStart();
            ButtonReset.Pressed += ButtonReset_Pressed;
            ButtonLeft.Pressed += (sender, _) => MoveButton_Press((Button)sender!, _game.MoveLeft);
            ButtonRight.Pressed += (sender, _) => MoveButton_Press((Button)sender!, _game.MoveRight);
            ButtonDown.Pressed += (sender, _) => MoveButton_Press((Button)sender!, _game.MoveDown);
            ButtonRotate.Pressed += (_, _) => _game.Rotate();
        }

        private void HandleGameEvents()
        {
            _game.FigureMoved += MoveFigure;
            _game.GameOver += Gameover;
            _game.FigureChanged += Game_FigureChanged;
            _game.ScoreChanged += Game_ScoreChanged;
            _game.LevelChanged += Game_LevelChanged;
            _game.GlassLinesRemoved += Game_GlassLinesRemoved;
        }

        private void Game_FigureChanged()
        {
            ClearGlass(GridNext);
            DrawFigure(GridNext, _game.NextFigure);
        }

        private void Game_GlassLinesRemoved()
        {
            Border[,] borders = GetBorderList(GridGlass);
            for (int i = 0; i < _game.TetrisGlass.Size.Width; i++)
            {
                for (int j = 0; j < _game.TetrisGlass.Size.Height; j++)
                {
                    borders[i, j].Background = _game.TetrisGlass[i, j] ? MauiColor.FromArgb("#141414") : MauiColor.FromArgb("#919191");
                    borders[i, j].Stroke = _game.TetrisGlass[i, j] ? MauiColor.FromArgb("#141414") : MauiColor.FromArgb("#6E6E6E");
                }
            }
        }

        private void Game_LevelChanged() => LabelLevel.Text = "Level " + _game.Level;

        private void Game_ScoreChanged() => LabelScore.Text = _game.Score.ToString("000000");

        private void ButtonReset_Pressed(object? sender, EventArgs e)
        {
            _mainTimer?.Stop();
            _game.CreateNewGame();

            ClearGlass(GridGlass);
            ClearGlass(GridNext);

            _mainTimer?.Start();
        }

        private static void ClearGlass(Grid grid)
        {
            Size size = GetGridSize(grid);
            Border[,] bordersNext = GetBorderList(grid);

            for (int i = 0; i < size.Width; i++)
            {
                for (int j = 0; j < size.Height; j++)
                {
                    bordersNext[i, j].Background = MauiColor.FromArgb("#919191");
                    bordersNext[i, j].Stroke = MauiColor.FromArgb("#6E6E6E");
                }
            }
        }

        private void MoveButton_Press(Button button, Action action)
        {
            action();
            _actionTimer!.Tick += actionTimer_Tick;
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

        private static void InitializeGlass(Grid grid, int width, int height)
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
            if (_newGame)
            {
                ClearFigure(GridGlass, _game.CurrentFigure.Coordinates);
                ClearFigure(GridNext, _game.NextFigure.Coordinates);
                _game.CreateNewGame();
                DrawFigure(GridGlass, _game.CurrentFigure);
                DrawFigure(GridNext, _game.NextFigure);
                LabelGameover.Text = string.Empty;
                _newGame = false;
            }

            _mainTimer?.Start();
        }

        private void MoveFigure(List<Coordinate> oldCoordinates)
        {
            ClearFigure(GridGlass, oldCoordinates);
            DrawFigure(GridGlass, _game.CurrentFigure);
        }

        private void Gameover()
        {
            _newGame = true;
            _mainTimer?.Stop();
            LabelGameover.Text = "GAME OVER";
        }

        private static void ClearFigure(Grid grid, List<Coordinate> oldCoordinates)
        {
            Border[,] borders = GetBorderList(grid);
            Size gridSize = GetGridSize(grid);

            foreach (Coordinate oldCoordinate in oldCoordinates)
            {
                bool prevCoordinateIsValid = oldCoordinate.X >= 0 && oldCoordinate.Y >= 0 && oldCoordinate.X < gridSize.Width && oldCoordinate.Y < gridSize.Height;

                if (prevCoordinateIsValid)
                {
                    borders[oldCoordinate.X, oldCoordinate.Y].Background = MauiColor.FromArgb("#919191");
                    borders[oldCoordinate.X, oldCoordinate.Y].Stroke = MauiColor.FromArgb("#6E6E6E");
                }
            }
        }

        private static void DrawFigure(Grid grid, Figure figure)
        {
            Border[,] borders = GetBorderList(grid);
            Size gridSize = GetGridSize(grid);

            foreach (Coordinate coordinate in figure.Coordinates)
            {
                bool isCoordinateValid = coordinate.X >= 0 && coordinate.Y >= 0 && coordinate.X < gridSize.Width && coordinate.Y < gridSize.Height;
                if (isCoordinateValid)
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
