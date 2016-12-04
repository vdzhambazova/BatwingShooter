using BatwingShooter.GameObjects;

namespace BatwingShooter.Misc
{
    public class ComplexCollisionDetector : SimpleCollisionDetector
    {
        public override bool AreCollided(GameObject go1, GameObject go2)
        {
            if (base.AreCollided(go1, go2))
            {
                return true;
            }
            var go1Bounds = GetObjectBounds(go1);
            var go2Bounds = GetObjectBounds(go2);

            bool shouldDie = CheckforInsideCollision(go1Bounds, go2Bounds);
            return shouldDie;
        }

        private bool CheckforInsideCollision(GameObjectBounds go1Bounds, GameObjectBounds go2Bounds)
        {
            return false;
        }
    }
}
