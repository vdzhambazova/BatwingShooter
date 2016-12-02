using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Windows.Threading;
using BatwingShooter.GameObjects;
using BatwingShooter.Misc;
using BatwingShooter.Renderer;

namespace BatwingShooter.Engines
{
    public class GameEngine
    {
        private const int BatwingWidth = 100;
        private const int BatwingHeight = 100;
        private const int TimerTickIntervalInMilliSeconds = 100;
        private const int BatmanSpeed = 15;

        private IRenderer renderer;

        static Random rand = new Random();
        

        public GameEngine(IRenderer renderer)
        {
            this.renderer = renderer;
            this.renderer.UiActionHappened += this.HandleUiActionHappened;
            this.Projectiles = new List<GameObject>();
        }

        public IList<GameObject> Projectiles { get; set; }

        private void HandleUiActionHappened(object sender, KeyDownEventArgs kdea)
        {
            if (kdea.Command == GameCommand.MoveDown)
            {
                var top = this.Batwing.Position.Top + BatmanSpeed;
                var left = this.Batwing.Position.Left;

                this.Batwing.Position = new Position(left, top);

            }
            else if (kdea.Command == GameCommand.MoveUp)
            {
                var top = this.Batwing.Position.Top - BatmanSpeed;
                var left = this.Batwing.Position.Left;

                this.Batwing.Position = new Position(left, top);
            }
            else if (kdea.Command == GameCommand.Fire)
            {
                this.FireProjectile();
            }
        }

        private void FireProjectile()
        {
            var projectileTop = new Projectile()
            {
                Position = new Position(this.Batwing.Position.Left+ this.Batwing.Bounds.Width, this.Batwing.Position.Top),
                Bounds = new Size(50,15)
            };

            var projectileBottom = new Projectile()
            {
                Position = new Position(this.Batwing.Position.Left + this.Batwing.Bounds.Width, this.Batwing.Position.Top + this.Batwing.Bounds.Height),
                Bounds = new Size(50, 15)
            };

            this.Projectiles.Add(projectileTop);
            this.Projectiles.Add(projectileBottom);
        }

        public void InitGame()
        {
            this.Batwing = new Batwing
            {
                Position = new Position(0, (this.renderer.ScreenHeight - BatwingHeight) / 2),
                Bounds = new Size(BatwingWidth, BatwingHeight),
            };

            this.Projectiles.Clear();
        }

        public Batwing Batwing { get; set; }

        public void StartGame()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(TimerTickIntervalInMilliSeconds);
            timer.Tick += this.GameLoop;

            timer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            this.renderer.Clear();
            this.renderer.Draw(this.Batwing);

            foreach (var projectile in Projectiles)
            {
                var top = projectile.Position.Top;
                var left = projectile.Position.Left + 55;

                projectile.Position = new Position(left, top);
                this.renderer.Draw(projectile);
            }
        }
    }
}
