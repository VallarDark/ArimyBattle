namespace ArmyBattle.Skills
{
    using Abstraction;
    using System.Collections.Generic;
    public class SingleCleanup : StrangerSkill
    {
        public SingleCleanup( List<Warrior> targets, ISkill innerSkill = null) : base(targets,
            innerSkill)
        {
            RollbackTime = 3;
            CastTime = 2;

            ActionType = ActionTypeEnum.Buff;
            CountType = CountTypeEnum.Single;
        }

        protected override void SkillLogic(Warrior caster)
        {
            foreach (var target in GetTarget(caster))
            {
                target.ResetCharacteristics();
            }
        }
    }
}
