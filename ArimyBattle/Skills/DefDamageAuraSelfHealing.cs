namespace ArmyBattle.Skills
{
    using Abstraction;
    using System.Collections.Generic;

    /// <summary>
    /// Deals periodic damage to enemies within a radius of the target.
    /// Periodically increases the defense of allies within the target's radius.
    /// Periodically increases its own HP.
    /// </summary>
    public class DefDamageAuraSelfHealing : StrangerSkill
    {
        public DefDamageAuraSelfHealing(List<Warrior> targets, ISkill innerSkill = null, int rollbackTime = 3, int castTime = 2, int strange = 7, int range = 20) : base(targets,innerSkill,rollbackTime, castTime, strange, range)
        {
            var healing = new SelfHealing(null,rollbackTime,castTime,strange);
            var damage = new DamageAura(targets,healing,rollbackTime,castTime,strange,range);
            var def = new DefAura(targets,damage,rollbackTime,castTime,strange,range);
            InnerSkill = def;
        }

        protected override void SkillLogic(Warrior caster)
        {
        }
    }
}
