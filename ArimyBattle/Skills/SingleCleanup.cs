namespace ArmyBattle.Skills
{
    using Abstraction;
    using System.Collections.Generic;

    /// <summary>
    /// Periodically removes all effects from other target.
    /// </summary>
    public class SingleCleanup : StrangerSkill
    {
        public SingleCleanup( List<Warrior> targets, ISkill innerSkill = null, int rollbackTime = 3, int castTime = 2, int strange = 0, int range = 20) :
            base(targets, innerSkill,rollbackTime, castTime, strange, range)
        {
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
