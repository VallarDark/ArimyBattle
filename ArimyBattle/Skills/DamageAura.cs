namespace ArmyBattle.Skills
{
    using Abstraction;
    using System.Collections.Generic;
    using System;

    /// <summary>
    ///  Deals periodic damage to enemies within a radius of the target.
    /// </summary>
    public class DamageAura : StrangerSkill
    {
        public DamageAura( List<Warrior> targets, ISkill innerSkill = null,int rollbackTime = 3, int castTime = 2, int strange = 5,int range=20) : base( targets, innerSkill, rollbackTime, castTime, strange, range)
        {
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
