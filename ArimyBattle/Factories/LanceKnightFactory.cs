namespace ArmyBattle.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using Abstraction;
    using Skills;
    using Warriors;
    public class LanceKnightFactory : WarriorFactory
    {
        protected override Warrior SetPrototype()
        {
            return new LanceKnight(
                allUnits: AllWarriors,
                position: new Vector2(0, 0),
                teamNumber: 0,
                hp: 250,
                def: 4,
                attackPower: 15,
                attackRange: 15,
                attackResetTime: 2,
                skill: new DamageAura(AllWarriors),
                dominanceWarriors: new List<Type>()
                {
                    typeof(Swordsman)
                },
                suppressionWarriors: new List<Type>()
                {
                    typeof(Archer)
                }
                );
        }

        public LanceKnightFactory(List<Warrior> allWarriors = null) : base(allWarriors)
        {
        }
    }
}
