namespace ArmyBattle.Skills
{
    using Abstraction;
    using System.Collections.Generic;
    public class DefDamageAuraSelfHealing : SelfSkill
    {
        public DefDamageAuraSelfHealing(List<Warrior> targets, ISkill innerSkill = null) : base(innerSkill)
        {
            RollbackTime = 3;
            CastTime = 2;

            InnerSkill = new DefAura(targets, new DamageAura(targets));
        }

        protected override void SkillLogic(Warrior caster)
        {
            caster.Hp += 2;
        }
    }
}
