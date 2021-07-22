

using System.Linq;
using System.Numerics;

namespace ArmyBattle.Abstraction
{
    using System.Collections.Generic;
    public abstract class StrangerSkill : ISkill
    {
        protected enum CountTypeEnum
        {
            Single,
            Many
        }
        protected enum ActionTypeEnum
        {
            Buff,
            Debuff
        }

        protected delegate void Skill(Warrior caster);
        protected Skill SkillFunk;

        protected ActionTypeEnum ActionType;
        protected CountTypeEnum CountType;
        protected int RollbackTime;
        protected int CastTime;

        protected int Range;
        protected int Strange;
        protected ISkill InnerSkill;
        protected List<Warrior> Targets;
        protected Warrior Caster;

        protected int RollbackCounter;
        protected int CastCounter;

        protected StrangerSkill(Warrior caster, List<Warrior> targets, ISkill innerSkill = null)
        {
            Caster = caster;
            Targets = targets;
            InnerSkill = innerSkill;
        }

        protected List<Warrior> GetTarget()
        {
            var currentTargets = Targets.Where(t => Vector2.Distance(Caster.Position, t.Position) <= Range).ToList();
            if (ActionType == ActionTypeEnum.Buff)
            {
                currentTargets = currentTargets.Where(t => t.TeamNumber == Caster.TeamNumber).ToList();
            }
            else
            {
                currentTargets = currentTargets.Where(t => t.TeamNumber != Caster.TeamNumber).ToList();
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

        public void UseSkill(Warrior caster)
        {
            if (RollbackCounter <= 0)
            {
                if (CastCounter <= 0)
                {
                    SkillFunk(caster);
                    CastCounter = CastTime;
                }
                else
                {
                    CastCounter--;
                }

                RollbackCounter = RollbackTime;
            }
            else
            {
                RollbackCounter--;
            }

            InnerSkill?.UseSkill(caster);
        }
    }
}
