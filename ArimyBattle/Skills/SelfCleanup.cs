namespace ArmyBattle.Skills
{
    using Abstraction;
    public class SelfCleanup : SelfSkill
    {
        public SelfCleanup(ISkill innerSkill = null) : base(innerSkill)
        {
            RollbackTime = 3;
            CastTime = 2;
        }

        protected override void SkillLogic(Warrior caster)
        {
            caster.ResetCharacteristics();
        }
    }
}
