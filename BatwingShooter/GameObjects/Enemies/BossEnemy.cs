namespace BatwingShooter.GameObjects.Enemies
{
    class BossEnemy:BigEnemy
    {
        private const int DefaultHealth = 5;

        public BossEnemy()
            : base(DefaultHealth)
        {

        }
    }
}
