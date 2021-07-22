﻿namespace ArmyBattle.Skills
{
    using Abstraction;
    using System.Collections.Generic;

    class DefAura:StrangerSkill
    {
        public DefAura(Warrior caster, List<Warrior> targets, ISkill innerSkill = null) : base(caster, targets, innerSkill)
        {
            Strange = 5;
            RollbackTime = 3;
            CastTime = 2;

            ActionType = ActionTypeEnum.Buff;
            CountType = CountTypeEnum.Many;

            SkillFunk = (warrior) =>
            {
                foreach (var target in GetTarget())
                {
                    target.Def += Strange;
                }
            };
        }
    }
}
