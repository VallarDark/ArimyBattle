namespace ArmyBattle.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using Abstraction;
    using Skills;
    using Warriors;
    public class SwordsmanFactory: WarriorFactory
    {
        protected override Warrior SetPrototype()
        {
            return new Swordsman(
                allUnits: AllWarriors,
                position: new Vector2(0, 0),
                teamNumber: 0,
                hp: 300,
                def: 8,
                attackPower: 20,
                attackRange: 10,
                attackResetTime: 3,
                skill: new DefAura(AllWarriors),
                dominanceWarriors: new List<Type>()
                {
                    typeof(Archer)
                },
                suppressionWarriors: new List<Type>()
                {
                    typeof(LanceKnight)
                }
                );
        }

        public SwordsmanFactory(List<Warrior> allWarriors) : base(allWarriors)
        {
        }
    }
}
