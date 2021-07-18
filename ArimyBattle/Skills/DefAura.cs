namespace ArmyBattle.Skills
{
    using Abstraction;
    using System;
    class DefAura:AoeBuffSkill
    {
        private const int BuffStrange = 5;

        public DefAura()
        {
            Radius = 2;
        }

        public override void UseSkill(Warrior caster, params Warrior[] targets)
        {
            var currentTargets = GetTargets(caster, targets);
            for (var index = 0; index < currentTargets.Length; index++)
            {
                var target = currentTargets[index];
                target.Def = BuffStrange;
            }
        }
    }
}
