namespace ArmyBattle.Skills
{
    using Abstraction;
    using System;

    /// <summary>
    /// Periodically removes all effects from itself.
    /// </summary>
    public class SelfCleanup : SelfSkill
    {
        public SelfCleanup(ISkill innerSkill = null, int rollbackTime = 3, int castTime = 2, int strange = 10) : base(innerSkill,rollbackTime, castTime, strange)
        {
        }

        protected override void Log(Warrior caster, Warrior target = null)
        {
            Console.WriteLine($"_____SelfCleanup_____");
            Console.WriteLine($"Caster:\n{caster}");
        }

        protected override void SkillLogic(Warrior caster)
        {
            
            caster.ResetCharacteristics();
            Log(caster);
        }
    }
}
