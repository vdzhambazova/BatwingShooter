using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BatwingShooter.Misc;

namespace BatwingShooter.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using GameObjects;

    public class WpfRenderer : IRenderer
    {
        private Canvas canvas;

        public event EventHandler<KeyDownEventArgs> UiActionHappened;
        public WpfRenderer(Canvas canvas)
        {
            this.canvas = canvas;
            (this.canvas.Parent as MainWindow).KeyDown += (sender, args) =>
            {
                var key = args.Key;
                if (key == Key.Down)
                {
                    this.UiActionHappened(this, new KeyDownEventArgs(GameCommand.MoveDown));
                }
                else if (key == Key.Up)
                {
                    this.UiActionHappened(this, new KeyDownEventArgs(GameCommand.MoveUp));
                }
                else if (key == Key.Space)
                {
                    this.UiActionHappened(this, new KeyDownEventArgs(GameCommand.Fire));
                }
            };
        }

        public int ScreenWidth => (int)this.canvas.Width;
        public int ScreenHeight => (int)(this.canvas.Parent as MainWindow).Height;

        public void Draw(params GameObject[] gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                if (gameObject is Batwing)
                {
                    this.DrawBatwing(gameObject);
                }
                else if (gameObject is Enemy)
                {
                    this.DrawEnemy(gameObject);
                }
                else if (gameObject is Projectile)
                {
                    this.DrawProjectile(gameObject);
                }
            }

        }

        private void DrawProjectile(GameObject projectile)
        {
            var ell = new Rectangle()
            {
                Width = projectile.Bounds.Width,
                Height = projectile.Bounds.Height,
                Fill = Brushes.Red,
                StrokeThickness = 3
            };

            Canvas.SetLeft(ell, projectile.Position.Left);
            Canvas.SetTop(ell, projectile.Position.Top);
            this.canvas.Children.Add(ell);
        }

        private void DrawEnemy(GameObject enemy)
        {
            var ell = new Ellipse()
            {
                Width = enemy.Bounds.Width,
                Height = enemy.Bounds.Height,
                Fill = Brushes.Green,
                StrokeThickness = 3
            };

            Canvas.SetLeft(ell, enemy.Position.Left);
            Canvas.SetTop(ell, enemy.Position.Top);
            this.canvas.Children.Add(ell);
        }

        private void DrawBatwing(GameObject batwing)
        {

            Image batwingImage = new Image();
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("/Images/batwing.png", UriKind.Relative);
            image.EndInit();

            batwingImage.Source = image;
            batwingImage.Width = batwing.Bounds.Width;
            batwingImage.Height = batwing.Bounds.Height;

            Canvas.SetLeft(batwingImage, batwing.Position.Left);
            Canvas.SetTop(batwingImage, batwing.Position.Top);
            this.canvas.Children.Add(batwingImage);
        }

        public void Clear()
        {
            this.canvas.Children.Clear();
        }
    }
}
