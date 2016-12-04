namespace BatwingShooter.Renderer
{
    
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using GameObjects.Enemies;
    using System.Windows.Input;
    using System.Windows.Media;
    using Misc;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    using Windows;
    using GameObjects;

    public class WpfRenderer : IRenderer
    {
        private const string BatwingImagePath = "/Images/batwing.png";
        private const string YamatoImagePath = "/Images/projectiles/yamato.png";
        private const string EnemyImagePath = "/Images/enemies/enemy.png";
        private const string BossEnemyImagePath = "/Images/enemies/boss-enemy.png";

        public event EventHandler<KeyDownEventArgs> UiActionHappened;

        private Canvas canvas;

        public WpfRenderer(Canvas canvas)
        {
            this.canvas = canvas;
            this.ParentWindow.KeyDown += HandleKeyDown;
        }

        public void ShowEndGameScreen(int highScore)
        {
            new GameOverWindow(highScore).Show();
            ParentWindow.Close();
        }

       
        public Window ParentWindow
        {
            get
            {
                var parent = canvas.Parent;
                while (!(parent is Window))
                {
                    parent = LogicalTreeHelper.GetParent(parent);
                }
                return parent as Window;
            }
        }

        public int ScreenWidth => (int)ParentWindow.ActualWidth;

        public int ScreenHeight => (int)ParentWindow.Height;

        public void Draw(params GameObject[] gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                if (gameObject is Batwing)
                {
                    DrawBatwing(gameObject);
                }
                else if (gameObject is BossEnemy)
                {
                    this.DrawBossEnemy(gameObject);
                }
                else if (gameObject is Enemy)
                {
                    DrawEnemy(gameObject);
                }
                else if (gameObject is YamatoProjectile)
                {
                    DrawYamato(gameObject);
                }
                else if (gameObject is Projectile)
                {
                    DrawProjectile(gameObject);
                }
            }

        }

        private void DrawYamato(GameObject yamato)
        {
            var image = this.CreateImageForCanvas(YamatoImagePath, yamato.Position, yamato.Bounds);
            this.canvas.Children.Add(image);
        }

        private void DrawProjectile(GameObject projectile)
        {
            var rect = new Border
            {
                Width = projectile.Bounds.Width,
                Height = projectile.Bounds.Height,
                Background = Brushes.Orange,
                CornerRadius = new CornerRadius(2, 5, 5, 2)
            };

            Canvas.SetLeft(rect, projectile.Position.Left);
            Canvas.SetTop(rect, projectile.Position.Top);
            this.canvas.Children.Add(rect);
        }

        private void DrawEnemy(GameObject enemy)
        {
            var image = CreateImageForCanvas(EnemyImagePath, enemy.Position, enemy.Bounds);
            this.canvas.Children.Add(image);
        }

        private void DrawBossEnemy(GameObject enemy)
        {
            var image = CreateImageForCanvas(BossEnemyImagePath, enemy.Position, enemy.Bounds);
            this.canvas.Children.Add(image);
        }

        private void DrawBigEnemy(GameObject bigEnemy)
        {
            var enemy = new Rectangle
            {
                Fill = Brushes.Black,
                Width = bigEnemy.Bounds.Width,
                Height = bigEnemy.Bounds.Height
            };
            Canvas.SetLeft(enemy, bigEnemy.Position.Left);
            Canvas.SetTop(enemy, bigEnemy.Position.Top);
            this.canvas.Children.Add(enemy);
        }

        private void DrawBatwing(GameObject batwing)
        {
            var image = this.CreateImageForCanvas(BatwingImagePath, batwing.Position, batwing.Bounds);
            this.canvas.Children.Add(image);
        }

        public void Clear()
        {
            canvas.Children.Clear();
        }

        public bool IsInBounds(Position position)
        {
            return 0 <= position.Left && position.Left <= ScreenWidth &&
                   0 <= position.Top && position.Top <= ScreenHeight;
        }

        private Image CreateImageForCanvas(string path, Position position, GameObjects.Size bounds)
        {
            Image image = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();

            image.Source = bitmap;
            image.Width = bounds.Width;
            image.Height = bounds.Height;

            Canvas.SetLeft(image, position.Left);
            Canvas.SetTop(image, position.Top);
            return image;
        }

        private void HandleKeyDown(object sender, KeyEventArgs args)
        {
            var key = args.Key;
            GameCommand command;
            switch (key)
            {
                case Key.Up:
                    command = GameCommand.MoveUp;
                    break;
                case Key.Down:
                    command = GameCommand.MoveDown;
                    break;
                case Key.Left:
                    command = GameCommand.MoveLeft;
                    break;
                case Key.Right:
                    command = GameCommand.MoveRight;
                    break;
                case Key.Enter:
                    command = GameCommand.PlayPause;
                    break;
                default:
                    command = GameCommand.Fire;
                    break;
            }

            this.UiActionHappened(this, new KeyDownEventArgs(command));
        }

    }
}
