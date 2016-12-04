namespace BatwingShooter.GameObjects.Factories
{
    public interface IGameObjectFactroy
    {
        GameObject Get(int left, int top);
    }
}
