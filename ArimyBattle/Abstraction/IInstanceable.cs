namespace ArmyBattle.Abstraction
{
    using System.Numerics;
    public interface IInstanceable
    {
        public Warrior GetInstance(Warrior prototype,Vector2 position, int teamNumber);
    }
}
