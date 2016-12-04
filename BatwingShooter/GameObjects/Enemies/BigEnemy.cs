namespace BatwingShooter.GameObjects.Enemies
{
    public class BigEnemy : Enemy
    {
        private const int DefaultBigEnemyHealth = 3;

        protected int Health { get; set; }

        protected BigEnemy(int health)
        {
            Health = health;
        }

        public BigEnemy()
            :this(DefaultBigEnemyHealth)
        {
        }

        public override bool IsAlive
        {
            get
            {
                return Health > 0;
            }
            set
            {
                if (!value)
                {
                    --Health;
                }
            }
        }
    }
}