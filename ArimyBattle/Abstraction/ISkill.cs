namespace ArmyBattle.Abstraction
{
    public interface ISkill
    {
        public void UseSkill(Warrior caster, params Warrior[] targets);
    }
}
