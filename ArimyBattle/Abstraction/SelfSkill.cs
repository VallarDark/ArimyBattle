namespace ArmyBattle.Abstraction
{
    public abstract class SelfSkill : ISkill
    {
        public abstract void UseSkill(Warrior caster, params Warrior[] targets);
    }
}
