namespace ArmyBattle.Warriors
{
    using System;
    using System.Collections.Generic;
    using Abstraction;
    using System.Numerics;
    public sealed class Swordsman : Warrior
    {
        public override Warrior GetInstance(Vector2 position, int teamNumberParam)
        {
            return new Swordsman( position, teamNumberParam)
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


        public Swordsman(List<Warrior> allUnits, Vector2 position, int teamNumber, int hp, int def, int attackPower, int attackRange, int attackResetTime, ISkill skill, List<Type> dominanceWarriors, List<Type> suppressionWarriors) : base(allUnits, position, teamNumber, hp, def, attackPower, attackRange, attackResetTime, skill, dominanceWarriors, suppressionWarriors)
        {
        }

        public Swordsman(Vector2 position, int teamNumber) : base(position, teamNumber)
        {
        }
    }
}
