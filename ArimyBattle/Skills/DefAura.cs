using System.Collections.Generic;

namespace ArmyBattle.Skills
{
    using Abstraction;

    class DefAura:StrangerSkill
    {
        public DefAura( List<Warrior> targets) : base( targets)
        {
            Range = 2;
            Strange = 5;
            ActionType = ActionTypeEnum.Buff;
            CountType = CountTypeEnum.Many;
        }

        public override void UseSkill(Warrior caster)
        {
            foreach (var target in GetTarget())
            {
                target.Def += Strange;
            }

            base.UseSkill(caster);
        }

    }
}
