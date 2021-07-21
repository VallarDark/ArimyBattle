

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

        protected ActionTypeEnum ActionType { get; set; }
        protected CountTypeEnum CountType { get; set; }
        public int Range { get; set; }
        public int Strange { get; set; }
        public ISkill InnerSkill { get; set; }
        public List<Warrior> Targets { get; set; }
        public Warrior Caster { get; set; }


        protected StrangerSkill(List<Warrior> targets)
        {
            Targets = targets;
        }

        protected List<Warrior> GetTarget()
        {
            var currentTargets = Targets.Where(t=> Vector2.Distance(Caster.Position, t.Position)<=Range).ToList();
            if (ActionType == ActionTypeEnum.Buff)
            {
                currentTargets = currentTargets.Where(t => t.CommandNumber == Caster.CommandNumber).ToList();
            }
            else
            {
                currentTargets = currentTargets.Where(t => t.CommandNumber != Caster.CommandNumber).ToList();
            }
            
            if (CountType == CountTypeEnum.Single)
            {
                return new List<Warrior>() {currentTargets.FirstOrDefault()};
            }
            else
            {
                return currentTargets;
            }
        }

        public virtual void UseSkill(Warrior caster)
        {
            InnerSkill.UseSkill(caster);
        }

    }
}
