namespace ArmyBattle.Abstraction
{
    using System;
    /// <summary>
    /// The skill is applied to the caster itself.
    /// </summary>
    public abstract class SelfSkill : ISkill
    {
        protected int Strange;
        protected ISkill InnerSkill;
        protected int RollbackTime;
        protected int CastTime;

        protected int RollbackCounter;
        protected int CastCounter;

        protected SelfSkill(ISkill innerSkill = null)
        {
            InnerSkill = innerSkill;
        }

        protected virtual void Log(Warrior caster, Warrior target = null)
        {
            Console.WriteLine($"_____DefAura_____");
            Console.WriteLine($"Caster:\n{caster}");
            if (target == null)
            {
                return;
            }
            Console.WriteLine($"Target:\n{target}");
        }

        /// <summary>
        /// The internal logic of the ability that will be called upon activation.
        /// </summary>
        /// <param name="caster"> Warrior who apply the skill</param>
        protected abstract void SkillLogic(Warrior caster);

        /// <summary>
        /// Apply skill.
        /// </summary>
        /// <param name="caster"> Warrior who apply the skill </param>
        public void UseSkill(Warrior caster)
        {
            if (RollbackCounter <= 0)
            {
                if (CastCounter <= 0)
                {
                    SkillLogic(caster);
                    CastCounter = CastTime;
                }
                else
                {
                    CastCounter--;
                    RollbackCounter = RollbackTime;
                }
            }
            else
            {
                RollbackCounter--;
            }

            InnerSkill?.UseSkill(caster);
        }
    }
}
