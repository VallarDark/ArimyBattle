namespace ArmyBattle.Abstraction
{
    using System.Numerics;
    public interface IMoveable
    {
        public void Run(Vector2 direction);
        public void Rearrangement(Warrior target);
    }
}
