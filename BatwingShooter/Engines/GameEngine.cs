using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using BatwingShooter.Collections;
using BatwingShooter.GameObjects;
using BatwingShooter.GameObjects.Enemies;
using BatwingShooter.GameObjects.Factories;
using BatwingShooter.Misc;
using BatwingShooter.Renderer;

namespace BatwingShooter.Engines
{
    public class GameEngine : IGameEngine
    {
        private const int BatwingSizeHeight = 100;
        private const int BatwingSizeWidth = 100;
        private const int BatmanSpeed = 25;
        private const int TimerTickIntervalInMilliseconds = 100;
        private const int SpawnEnemyChange = 90;
        private const int ScoreForKill = 45;
        private const int ScoreForTick = 10;
        private const int ProjectileMoveSpeed = 105;
        private const int EnemyMoveSpeed = -25;

        private IRenderer renderer;
        private ProjectileFactory projectilesFactory;
        private EnemyFactory enemiesFactory;
        private DispatcherTimer timer;

        public int HighScore { get; private set; }
          
        static Random rand = new Random();

        public Batwing Batwing { get; private set; }

        public List<GameObject> Projectiles { get; private set; }

        public List<GameObject> Enemies { get; private set; }

        public List<GameObject> GameObjects { get; private set; }

        public ICollisionDetector CollisionDetector { get; private set; }

        Batwing IGameEngine.Batwing
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public GameEngine(IRenderer renderer)
        {
            this.renderer = renderer;
            this.renderer.UiActionHappened +=
                HandleUiActionHappened;

            this.Projectiles = new List<GameObject>();
            this.projectilesFactory = new ProjectileFactory();

            this.Enemies = new List<GameObject>();
            this.enemiesFactory = new EnemyFactory();

            this.GameObjects = new List<GameObject>();

            this.CollisionDetector = new ComplexCollisionDetector();
        }

        private void HandleUiActionHappened(object sender, KeyDownEventArgs e)
        {
            if (e.Command == GameCommand.Fire)
            {
                FireProjectile();
            }
            else if (e.Command == GameCommand.PlayPause)
            {
                PlayPauseGame();
            }
            else
            {
                int updateTop = 0;
                int updateLeft = 0;

                switch (e.Command)
                {
                    case GameCommand.MoveUp:
                        updateTop = -BatmanSpeed;
                        break;
                    case GameCommand.MoveDown:
                        updateTop = +BatmanSpeed;
                        break;
                    case GameCommand.MoveLeft:
                        updateLeft = -BatmanSpeed;
                        break;
                    case GameCommand.MoveRight:
                        updateLeft = +BatmanSpeed;
                        break;
                    default:
                        break;
                }

                int left = Batwing.Position.Left + updateLeft;
                int top = Batwing.Position.Top + updateTop;
                var position = new Position(left, top);
                if (renderer.IsInBounds(position))
                {
                    Batwing.Position = position;
                }
            }
        }

        private void PlayPauseGame()
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
        }

        private void FireProjectile()
        {
            int top = Batwing.Position.Top;
            int left = Batwing.Position.Left;
            var projectileTop = projectilesFactory.Get(left, top);
            var projectileBottom = projectilesFactory.Get(left, top + Batwing.Bounds.Height);
            Projectiles.Add(projectileTop);
            Projectiles.Add(projectileBottom);

            GameObjects.Add(projectileTop);
            GameObjects.Add(projectileBottom);
        }

        private void GameLoop(object sender, EventArgs e)
        {
            HighScore += ScoreForTick;
            if (Enemies.Any(enemy => CollisionDetector.AreCollided(Batwing, enemy)))
            {
                timer.Stop();
                renderer.ShowEndGameScreen(HighScore);
                return;
            }

            renderer.Clear();
            renderer.Draw(Batwing);

            if (rand.Next(100) < SpawnEnemyChange)
            {
                var enemy = enemiesFactory.Get(renderer.ScreenWidth, rand.Next(renderer.ScreenHeight));
                Enemies.Add(enemy);
                GameObjects.Add(enemy);
            }

            KillEnemiesIfColliding();

            HighScore += Enemies.Count(enemy => !enemy.IsAlive) * ScoreForKill;
            RemoveNotAliveGameObjects();
            UpdateObjectsPositions();
            DrawGameObjects();
        }

        private void KillEnemiesIfColliding()
        {
            foreach (var projectile in Projectiles)
            {
                foreach (var enemy in Enemies)
                {
                    if (CollisionDetector.AreCollided(projectile, enemy))
                    {
                        enemy.IsAlive = false;
                        projectile.IsAlive = false;
                        break;
                    }
                }
            }
        }

        private void UpdateObjectsPositions()
        {
            foreach (var go in GameObjects)
            {
                int top = 0;
                int left = 0;
                if (go is Projectile)
                {
                    top = go.Position.Top;
                    left = go.Position.Left + ProjectileMoveSpeed;
                }
                else if (go is Enemy)
                {
                    top = go.Position.Top + rand.Next(-10, 10);
                    left = go.Position.Left + EnemyMoveSpeed;
                }
                go.Position = new Position(left, top);
            }
        }

        private void DrawGameObjects()
        {
            GameObjects.ForEach(go => renderer.Draw(go));
        }

        private void RemoveNotAliveGameObjects()
        {
            GameObjects.Where(go => !renderer.IsInBounds(go.Position))
                .ForEach(go => go.IsAlive = false);

            GameObjects.RemoveAll(go => !go.IsAlive);
            Enemies.RemoveAll(enemy => !enemy.IsAlive);
            Projectiles.RemoveAll(projectile => !projectile.IsAlive);
        }

        public void InitGame()
        {
            Batwing = new Batwing
            {
                Position = new Position(0, (renderer.ScreenHeight - BatwingSizeHeight) / 2),
                Bounds = new Size(BatwingSizeWidth, BatwingSizeHeight),
            };
            Projectiles.Clear();
        }

        public void StartGame()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(TimerTickIntervalInMilliseconds);
            //game loop
            timer.Tick += GameLoop;
            timer.Start();
        }
    }
}
