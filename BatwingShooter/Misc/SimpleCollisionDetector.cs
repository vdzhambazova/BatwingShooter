using BatwingShooter.GameObjects;

namespace BatwingShooter.Misc
{
    public class SimpleCollisionDetector : ICollisionDetector
    {
        protected struct GameObjectBounds
        {
            public GameObjectBounds(Position topLeft, Position bottomRight)
                : this(topLeft.Left, topLeft.Top, bottomRight.Left, bottomRight.Top)
            {
            }

            public GameObjectBounds(int left, int top, int right, int bottom)
                : this()
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public int Left { get; set; }

            public int Right { get; set; }

            public int Top { get; set; }

            public int Bottom { get; set; }
        }

        public virtual bool AreCollided(GameObject go1, GameObject go2)
        {
            GameObjectBounds go1Bounds = GetObjectBounds(go1);

            GameObjectBounds go2Bounds = GetObjectBounds(go2);

            bool shouldDie = SimpleCollision(go1Bounds, go2Bounds);
            return shouldDie;
        }

        protected GameObjectBounds GetObjectBounds(GameObject go1)
        {
            int go1Left = go1.Position.Left;
            int go1Right = go1.Position.Left + go1.Bounds.Width;
            int go1Top = go1.Position.Top;
            int go1Bottom = go1.Position.Top + go1.Bounds.Height;
            GameObjectBounds go1Bounds = new GameObjectBounds(go1Left, go1Top, go1Right, go1Bottom);
            return go1Bounds;
        }

        private bool SimpleCollision(GameObjectBounds go1Bounds, GameObjectBounds go2Bounds)
        {
            return ((go1Bounds.Top <= go2Bounds.Top && go2Bounds.Top <= go1Bounds.Bottom) || (go1Bounds.Top <= go2Bounds.Bottom && go2Bounds.Bottom <= go1Bounds.Bottom)) &&
                   ((go1Bounds.Left <= go2Bounds.Left && go2Bounds.Left <= go1Bounds.Right) || (go1Bounds.Left <= go2Bounds.Right && go2Bounds.Right <= go1Bounds.Right));
        }
    }
}