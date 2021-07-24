namespace ArmyBattle.Warriors
{
    using System;
    using System.Collections.Generic;
    using Abstraction;
    using System.Numerics;
    public sealed class Swordsman : Warrior
    {
        public override Warrior GetInstance(Warrior prototype, Vector2 position, int teamNumber)
        {
            return new Swordsman(
                prototype.AllUnits,
                position,
                teamNumber,
                prototype.Hp,
                prototype.Def,
                prototype.AttackPower,
                prototype.AttackRange,
                prototype.AttackResetTime,
                prototype.Skill,
                prototype.DominanceWarriors,
                prototype.SuppressionWarriors
            );
        }

        public Swordsman(List<Warrior> allUnits, Vector2 position, int teamNumber, int hp, int def,
            int attackPower, int attackRange, int attackResetTime, ISkill skill, List<Type> dominanceWarriors,
            List<Type> suppressionWarriors) : base(allUnits, position, teamNumber, hp, def, attackPower,
            attackRange, attackResetTime, skill, dominanceWarriors, suppressionWarriors)
        {
        }
    }
}
