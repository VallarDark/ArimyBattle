namespace ArmyBattle.Skills
{
    using Abstraction;
    using System.Collections.Generic;

    /// <summary>
    /// Deals periodic damage to enemies within a radius of the target.
    /// Periodically increases the defense of allies within the target's radius.
    /// Periodically increases its own HP.
    /// </summary>
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
