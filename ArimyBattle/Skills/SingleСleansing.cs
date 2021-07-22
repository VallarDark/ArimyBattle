namespace ArmyBattle.Skills
{
    using Abstraction;
    using System.Collections.Generic;
    class SingleСleansing : StrangerSkill
    {
        public SingleСleansing(Warrior caster, List<Warrior> targets, ISkill innerSkill = null) : base(caster, targets,
            innerSkill)
        {
            RollbackTime = 3;
            CastTime = 2;

            ActionType = ActionTypeEnum.Buff;
            CountType = CountTypeEnum.Single;

            SkillFunk = (warrior) =>
            {
                foreach (var target in GetTarget())
                {
                    target.ResetCharacteristics();
                }
            };
        }
    }
}
