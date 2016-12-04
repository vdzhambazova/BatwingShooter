using BatwingShooter.GameObjects;

namespace BatwingShooter.Misc
{
    public interface ICollisionDetector
    {
        bool AreCollided(GameObject go1, GameObject go2);
    }
}