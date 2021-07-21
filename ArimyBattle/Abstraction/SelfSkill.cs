namespace ArmyBattle.Abstraction
{
    public abstract class SelfSkill : ISkill
    {
        public int Strange { get; set; }
        public ISkill InnerSkill { get; set; }

        public virtual void UseSkill(Warrior caster)
        {
            InnerSkill.UseSkill(caster);
        }

    }
}
