namespace ArmyBattle.Abstraction
{
    public interface ISkillable
    {
        public void UseSkill(params Warrior[] targets);
    }
}
