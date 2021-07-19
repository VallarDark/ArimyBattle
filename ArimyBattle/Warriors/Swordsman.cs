namespace ArmyBattle.Warriors
{
    using System;
    using System.Collections.Generic;
    using Abstraction;
    using System.Numerics;
    public sealed class Swordsman : Warrior
    {
        public override Warrior GetInstance(Vector2 position, int commandNumber)
        {
            return new Swordsman( position, commandNumber)
            {
                AllUnits = AllUnits,
                AttackPower = AttackPower,
                AttackResetTime = AttackResetTime,
                AttackRange = AttackRange,
                Hp = Hp,
                Def = Def,
                Skill = Skill,
                DominanceWarriors = DominanceWarriors,
                SuppressionWarriors = SuppressionWarriors
            };
        }


        public Swordsman(List<Warrior> allUnits, Vector2 position, int commandNumber, int hp, int def, int attackPower, int attackRange, int attackResetTime, ISkill skill, List<Type> dominanceWarriors, List<Type> suppressionWarriors) : base(allUnits, position, commandNumber, hp, def, attackPower, attackRange, attackResetTime, skill, dominanceWarriors, suppressionWarriors)
        {
        }

        public Swordsman(Vector2 position, int commandNumber) : base(position, commandNumber)
        {
        }
    }
}
