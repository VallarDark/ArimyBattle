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

        /// <param name="targets"> List of all warriors in battle</param>
        /// <param name="innerSkill">An internal skill that will also be applied.</param>
        public DamageAura( List<Warrior> targets, ISkill innerSkill = null) : base( targets, innerSkill)
        {
            Strange = 5;
            RollbackTime = 3;
            CastTime = 2;
            Range = 20;

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
