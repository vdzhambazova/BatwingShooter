using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatwingShooter.GameObjects.Enemies;

namespace BatwingShooter.GameObjects.Factories
{
    public class EnemyFactory : IGameObjectFactroy
    {
        private const int EnemyWidth = 65;
        private const int EnemyHeight = 65;
        public GameObject Get(int left, int top)
        {
            return new Enemy()
            {
                Position = new Position(left, top),
                Bounds = new Size(EnemyWidth,EnemyHeight)
            };
        }
    }
}
