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

        /// <param name="innerSkill"> Internal skill that will also be performed </param>
        /// <param name="rollbackTime"> Ability cooldown </param>
        /// <param name="castTime"> Casting time of the ability </param>
        /// <param name="strange"> Power of ability </param>
        protected SelfSkill(ISkill innerSkill = null, int rollbackTime = 3, int castTime = 2, int strange = 3)
        {
            InnerSkill = innerSkill;
            Strange = strange;
            RollbackTime = rollbackTime;
            CastTime = castTime;
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
