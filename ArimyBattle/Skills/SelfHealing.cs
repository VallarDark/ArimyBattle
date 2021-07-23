namespace ArmyBattle.Skills
{
    using Abstraction;
    public class SelfHealing : SelfSkill
    {
        public SelfHealing(ISkill innerSkill = null) : base(innerSkill)
        {
            RollbackTime = 3;
            CastTime = 2;
        }

        protected override void SkillLogic(Warrior caster)
        {
            caster.Hp += 2;
        }
    }
}
