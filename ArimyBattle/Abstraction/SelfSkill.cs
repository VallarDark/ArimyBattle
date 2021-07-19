namespace ArmyBattle.Abstraction
{
    public abstract class SelfSkill : ISkill
    {
        public int Strange { get; set; }
        public abstract void UseSkill(Warrior caster);

    }
}
