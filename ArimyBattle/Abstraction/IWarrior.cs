
namespace ArmyBattle.Abstraction
{
    public interface IWarrior:IDamageable,ISkillable,IMoveable,IDieable,IInstanceable
    {
        public void Action();
    }
}
