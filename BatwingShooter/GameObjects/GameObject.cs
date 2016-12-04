namespace BatwingShooter.GameObjects
{
    public abstract class GameObject
    {
        public GameObject()
        {
            IsAlive = true;
        }

        public Size Bounds { get; set; }

        public Position Position { get; set; }

        public virtual bool IsAlive { get; set; }   
    }
}
