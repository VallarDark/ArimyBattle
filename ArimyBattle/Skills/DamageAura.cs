using System.Collections.Generic;

namespace ArmyBattle.Skills
{
    using Abstraction;

    class DamageAura : StrangerSkill
    {
        public DamageAura( List<Warrior> targets) : base( targets)
        {
            Range = 2;
            Strange = 5;
            ActionType = ActionTypeEnum.Debuff;
            CountType = CountTypeEnum.Many;
        }

        public override void UseSkill(Warrior caster)
        {
            foreach (var target in GetTarget())
            {
                target.Hp -= 30;
            }

            base.UseSkill(caster);
        }

    }
}
