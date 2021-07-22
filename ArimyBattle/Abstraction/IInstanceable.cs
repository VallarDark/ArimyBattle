namespace ArmyBattle.Abstraction
{
    using System.Numerics;
    public interface IInstanceable
    {
        public Warrior GetInstance(Vector2 position, int teamNumber);
    }
}
