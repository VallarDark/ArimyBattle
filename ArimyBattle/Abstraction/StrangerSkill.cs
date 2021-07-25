namespace ArmyBattle.Abstraction
{
    using System;
    using System.Linq;
    using System.Numerics;
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

        protected abstract void SkillLogic(Warrior caster);
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
