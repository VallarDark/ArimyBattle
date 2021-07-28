namespace ArmyBattle.Skills
{
    using Abstraction;
    using System.Collections.Generic;
    using System;

    /// <summary>
    ///  Periodically increases the defense of allies within the target's radius.
    /// </summary>
    public class DefAura:StrangerSkill
    {
        public DefAura(List<Warrior> targets, ISkill innerSkill = null, int rollbackTime = 3, int castTime = 2, int strange = 5, int range = 20) : base(targets, innerSkill,rollbackTime, castTime, strange, range)
        {
            ActionType = ActionTypeEnum.Buff;
            CountType = CountTypeEnum.Many;
        }

        protected override void Log(Warrior caster, Warrior target = null)
        {
            Console.WriteLine($"_____DefAura_____");
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
                target.Def += Strange;
                Log(caster,target);
            }
        }
    }
}
