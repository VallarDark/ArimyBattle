namespace ArmyBattle.Skills
{
    using Abstraction;
    using System.Collections.Generic;

    class DamageAura : StrangerSkill
    {
        public DamageAura(Warrior caster, List<Warrior> targets, ISkill innerSkill = null) : base(caster, targets, innerSkill)
        {
            Strange = 5;
            RollbackTime = 3;
            CastTime = 2;

            ActionType = ActionTypeEnum.Debuff;
            CountType = CountTypeEnum.Many;

            SkillFunk = (warrior) =>
            {
                foreach (var target in GetTarget())
                {
                    target.Hp -= Strange;
                }
            };
        }
    }
}
