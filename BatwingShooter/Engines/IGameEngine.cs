using System.Collections.Generic;
using BatwingShooter.GameObjects;
using BatwingShooter.Misc;

namespace BatwingShooter.Engines
{
    public interface IGameEngine
    {
        int HighScore { get; }

        Batwing Batwing { get; }

        List<GameObject> Projectiles { get; }

        List<GameObject> Enemies { get; }

        ICollisionDetector CollisionDetector { get;}

        void InitGame();

        void StartGame();
    }
}