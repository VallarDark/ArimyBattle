namespace ArmyBattle.Skills
{
    using Abstraction;
    using System.Collections.Generic;
    using System;

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

        protected override void Log(Warrior caster, Warrior target = null)
        {
            Console.WriteLine($"_____DamageAura_____");
            Console.WriteLine($"Caster:\n{caster}");
            if (target == null)
            {
                return;
            }
            Console.WriteLine($"Target:\n{target}");
        }

        protected override void SkillLogic(Warrior caster)
        {
            foreach (var target in GetTarget(caster))
            {
                target.Hp -= Strange;
                Log(caster,target);
            }
        }
    }
}
