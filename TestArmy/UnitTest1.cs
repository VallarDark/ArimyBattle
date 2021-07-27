using System.Linq;
using ArmyBattle.Abstraction;

namespace TestArmy
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using ArmyBattle.Army;
    using ArmyBattle.Factories;
    using ArmyBattle.Skills;
    using ArmyBattle.Warriors;
    using Xunit;
    public class UnitTest1
    {
        [Theory]
        [InlineData(2, 2, 2)]
        [InlineData(0, 7, 0)]
        [InlineData(-6, 2, 7)]
        public void BattleAddWarriorTest(int x, int y, int teamNumber)
        {
            // Arrange
            var position = new Vector2(x, y);
            Battle battle = new Battle();
            var resultWarrior = new Swordsman(
                allUnits: null,
                position: new Vector2(x, y),
                teamNumber: teamNumber,
                hp: 200,
                def: 5,
                attackPower: 10,
                attackRange: 1,
                attackResetTime: 3,
                skill: new DefAura(null),
                dominanceWarriors: new List<Type>(),
                suppressionWarriors: new List<Type>()
            );

            // Act
            var testWarrior = battle.AddWarrior<SwordsmanFactory>(position, teamNumber); 
            var testWarrior2 = battle.AddWarrior<ArcherFactory>(position, teamNumber);

            // Assert
            Assert.NotNull(testWarrior);
            Assert.NotNull(testWarrior2);
            Assert.Equal(resultWarrior.GetType(), testWarrior.GetType());
            Assert.NotEqual(resultWarrior.GetType(), testWarrior2.GetType());
            Assert.IsNotType<Archer>(testWarrior);
            Assert.IsType<Archer>(testWarrior2);
            Assert.Equal(resultWarrior.TeamNumber, testWarrior.TeamNumber);
            Assert.Equal(resultWarrior.Position, testWarrior.Position);
        }

        [Fact]
        public void DefAuraTest()
        {
            var allWarriors = new List<Warrior>();
            var factory = new ArcherFactory(allWarriors);
            var defAura = new DefAura(allWarriors);

            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            allWarriors.Add(factory.CreateWarrior(Vector2.One, 2));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            allWarriors.Add(factory.CreateWarrior(new Vector2(0,5), 2));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            var caster = allWarriors.First();

            Assert.All(allWarriors.Skip(1).Where(w => Vector2.Distance(w.Position, caster.Position) <= defAura.GetRange && w.TeamNumber==caster.TeamNumber), w => Assert.Equal(w.Def, w.GetBaseDef));

            defAura.UseSkill(caster);

            Assert.All(allWarriors.Skip(1).Where(w=>Vector2.Distance(w.Position,caster.Position)<=defAura.GetRange && w.TeamNumber == caster.TeamNumber),w=>Assert.NotEqual(w.Def,w.GetBaseDef));

        }

        [Fact]
        public void DamageAuraTest()
        {
            var allWarriors = new List<Warrior>();
            var factory = new ArcherFactory(allWarriors);
            var damageAura= new DamageAura(allWarriors);

            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            allWarriors.Add(factory.CreateWarrior(Vector2.One, 3));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            allWarriors.Add(factory.CreateWarrior(new Vector2(0, 5), 3));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 3));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            var caster = allWarriors.First();

            Assert.All(allWarriors.Skip(1).Where(w => Vector2.Distance(w.Position, caster.Position) <= damageAura.GetRange && w.TeamNumber != caster.TeamNumber), w => Assert.Equal(w.Hp, w.GetBaseHp));

            damageAura.UseSkill(caster);

            Assert.All(allWarriors.Skip(1).Where(w => Vector2.Distance(w.Position, caster.Position) <= damageAura.GetRange && w.TeamNumber != caster.TeamNumber), w => Assert.NotEqual(w.Hp, w.GetBaseHp));
        }

        [Fact]
        public void SelfHealTest()
        {
            var factory = new ArcherFactory();
            var selfHealing = new SelfHealing();

            var caster = factory.CreateWarrior(Vector2.Zero, 3);
            caster.Hp -= 20;

            Assert.NotEqual(caster.Hp,caster.GetBaseHp);
            for (int i = 0; i < 50; i++)
            {
                selfHealing.UseSkill(caster);
            }

            Assert.Equal(caster.Hp, caster.GetBaseHp);
        }
    }
}
