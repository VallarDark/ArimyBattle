﻿namespace ArmyBattle.Skills
{
    using Abstraction;
    using System.Collections.Generic;

    public class DamageAura : StrangerSkill
    {
        public DamageAura( List<Warrior> targets, ISkill innerSkill = null) : base( targets, innerSkill)
        {
            Strange = 5;
            RollbackTime = 3;
            CastTime = 2;

            ActionType = ActionTypeEnum.Debuff;
            CountType = CountTypeEnum.Many;
        }

        protected override void SkillLogic(Warrior caster)
        {
            foreach (var target in GetTarget(caster))
            {
                target.Hp -= Strange;
            }
        }
    }
}
