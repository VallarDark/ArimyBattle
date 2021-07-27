namespace ArmyBattle.Abstraction
{
    using System;
    using System.Linq;
    using System.Numerics;
    using System.Collections.Generic;

    /// <summary>
    /// The skill can be used on one or more targets other than the cast one.
    /// </summary>
    public abstract class StrangerSkill : ISkill
    {

        /// <summary>
        /// Ability type (single target or multiple targets)
        /// </summary>
        protected enum CountTypeEnum
        {
            Single,
            Many
        }

        /// <summary>
        /// Ability type (applies to allies or enemies)
        /// </summary>
        protected enum ActionTypeEnum
        {
            /// <summary>
            /// Applies to allies
            /// </summary>
            Buff,
            /// <summary>
            /// Applies to enemies
            /// </summary>
            Debuff
        }

        protected ActionTypeEnum ActionType;
        protected CountTypeEnum CountType;

        protected int RollbackTime;
        protected int CastTime;

        protected int Range;
        protected int Strange;
        protected ISkill InnerSkill;
        protected List<Warrior> Targets;

        protected int RollbackCounter;
        protected int CastCounter;

        public int GetRange => Range;
        public int GetStrange => Strange;

        protected StrangerSkill(List<Warrior> targets, ISkill innerSkill = null)
        {
            Targets = targets;
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

        protected List<Warrior> GetTarget(Warrior caster)
        {
            var currentTargets = Targets.Where(t => Vector2.Distance(caster.Position, t.Position) <= Range && t != caster).ToList();
            if (ActionType == ActionTypeEnum.Buff)
            {
                currentTargets = currentTargets.Where(t => t.TeamNumber == caster.TeamNumber).ToList();
            }
            else
            {
                currentTargets = currentTargets.Where(t => t.TeamNumber != caster.TeamNumber).ToList();
            }

            if (CountType == CountTypeEnum.Single)
            {
                return new List<Warrior>() { currentTargets.FirstOrDefault() };
            }
            else
            {
                return currentTargets;
            }
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
