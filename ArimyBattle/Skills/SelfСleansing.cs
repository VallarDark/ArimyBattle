namespace ArmyBattle.Skills
{
    using Abstraction;
    class SelfСleansing : SelfSkill
    {
        public SelfСleansing(Warrior caster, ISkill innerSkill = null) : base(caster, innerSkill)
        {
            RollbackTime = 3;
            CastTime = 2;

            SkillFunk = (warrior) =>
            {
                caster.ResetCharacteristics();
            };
        }
    }
}
