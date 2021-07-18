namespace ArmyBattle.Abstraction
{
    public abstract class StrangerSkill : ISkill
    {
        public int Radius { get; set; }
        public abstract void UseSkill(Warrior caster, params Warrior[] targets);
    }
}
