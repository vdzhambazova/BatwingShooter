namespace BatwingShooter.GameObjects
{
    public struct Size
    {
        public Size(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
        public int Width { get; set; }

        public int Height { get; set; }
    }
}
