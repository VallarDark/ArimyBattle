﻿namespace ArmyBattle.Skills
{
    using Abstraction;
    using System;

    /// <summary>
    /// Periodically increases its own HP.
    /// </summary>
    public class SelfHealing : SelfSkill
    {
        public SelfHealing(ISkill innerSkill = null) : base(innerSkill)
        {
            Strange = 10;
            RollbackTime = 3;
            CastTime = 2;
        }

        protected override void Log(Warrior caster, Warrior target = null)
        {
            Console.WriteLine($"_____SelfHealing_____");
            Console.WriteLine($"Caster:\n{caster}");

        }
        protected override void SkillLogic(Warrior caster)
        {
            caster.Hp += Strange;
            Log(caster);
        }
    }
}
