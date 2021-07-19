namespace ArmyBattle.Abstraction
{
    public interface ISkill
    {
        public int Strange { get; set; }
        public void UseSkill(Warrior caster);
    }
}
