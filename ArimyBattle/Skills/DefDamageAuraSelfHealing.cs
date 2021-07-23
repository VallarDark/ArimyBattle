namespace ArmyBattle.Skills
{
    using Abstraction;
    using System.Collections.Generic;
    public class DefDamageAuraSelfHealing : SelfSkill
    {
        public DefDamageAuraSelfHealing(List<Warrior> targets, ISkill innerSkill = null) : base(innerSkill)
        {
            InnerSkill = new DefAura(targets, new DamageAura(targets,new SelfHealing()));
        }

        protected override void SkillLogic(Warrior caster)
        {

        }
    }
}
