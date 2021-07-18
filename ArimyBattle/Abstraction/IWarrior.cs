namespace ArmyBattle.Abstraction
{
    using System.Numerics;

    public interface IWarrior
    {
         public void Attack(Warrior target);
         public void UseSkill(params Warrior[] targets);
         public void Run(Vector2 direction);
         public void Rearrangement(Warrior target);
         public void HpCheck();
    }
}
