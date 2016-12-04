using System;

namespace BatwingShooter.GameObjects.Factories
{
    public class ProjectileFactory: IGameObjectFactroy
    {
        private const int ProjectileWidth = 55;
        private const int ProjectileHeight = 15;

        private const int YamatoProjectileWidth = 165;
        private const int YamatoProjectileHeight = 35;

        private const int YamatoChance = 15;

        Random random = new Random();

        public GameObject Get(int left, int top)
        {
            int choice = random.Next(100);

            if (choice<YamatoChance)
            {
                return new YamatoProjectile()
                {
                    Position = new Position(left, top),
                    Bounds = new Size(YamatoProjectileWidth, YamatoProjectileHeight)
                };
            }
            
            return new Projectile()
            {
                Position = new Position(left, top),
                Bounds = new Size(ProjectileWidth, ProjectileHeight)
            };
        }
    }
}
