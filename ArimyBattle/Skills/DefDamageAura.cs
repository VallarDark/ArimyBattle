namespace ArmyBattle.Skills
{
    using Abstraction;
    using System.Collections.Generic;
    class DefDamageAura : StrangerSkill
    {
        public DefDamageAura(Warrior caster, List<Warrior> targets, ISkill innerSkill = null) : base(caster, targets, innerSkill)
        {
            InnerSkill = new DefAura(caster,targets, new DamageAura(caster,targets));
        }
    }
}
