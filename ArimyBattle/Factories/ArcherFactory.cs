

namespace ArmyBattle.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using Abstraction;
    using Skills;
    using Warriors;
    public class ArcherFactory : WarriorFactory
    {
        protected override Warrior SetPrototype()
        {
            return new Archer(
                allUnits: AllWarriors,
                position: new Vector2(0, 0),
                teamNumber: 0,
                hp: 100,
                def: 2,
                attackPower: 15,
                attackRange: 5,
                attackResetTime: 2,
                skill: new SelfCleanup(),
                dominanceWarriors: new List<Type>()
                {
                    typeof(LanceKnight)
                },
                suppressionWarriors: new List<Type>()
                {
                    typeof(Swordsman)
                }
                );
        }

        public ArcherFactory(List<Warrior> allWarriors) : base(allWarriors)
        {
        }
    }
}
