using System.Linq;
using System.Threading;
using ArmyBattle.Abstraction;
using static Xunit.Assert;

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
            NotNull(testWarrior);
            NotNull(testWarrior2);
            Equal(resultWarrior.GetType(), testWarrior.GetType());
            NotEqual(resultWarrior.GetType(), testWarrior2.GetType());
            IsNotType<Archer>(testWarrior);
            IsType<Archer>(testWarrior2);
            Equal(resultWarrior.TeamNumber, testWarrior.TeamNumber);
            Equal(resultWarrior.Position, testWarrior.Position);
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
            allWarriors.Add(factory.CreateWarrior(new Vector2(0, 5), 2));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            var caster = allWarriors.First();

            All(allWarriors.Skip(1).Where(w => Vector2.Distance(w.Position, caster.Position) <= defAura.GetRange && w.TeamNumber == caster.TeamNumber), w => Equal(w.Def, w.GetBaseDef));

            defAura.UseSkill(caster);

            All(allWarriors.Skip(1).Where(w => Vector2.Distance(w.Position, caster.Position) <= defAura.GetRange && w.TeamNumber == caster.TeamNumber), w => NotEqual(w.Def, w.GetBaseDef));

        }

        [Fact]
        public void DamageAuraTest()
        {
            var allWarriors = new List<Warrior>();
            var factory = new ArcherFactory(allWarriors);
            var damageAura = new DamageAura(allWarriors);

            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            allWarriors.Add(factory.CreateWarrior(Vector2.One, 3));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            allWarriors.Add(factory.CreateWarrior(new Vector2(0, 5), 3));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 3));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            var caster = allWarriors.First();

            All(allWarriors.Skip(1).Where(w => Vector2.Distance(w.Position, caster.Position) <= damageAura.GetRange && w.TeamNumber != caster.TeamNumber), w => Equal(w.Hp, w.GetBaseHp));

            damageAura.UseSkill(caster);

            All(allWarriors.Skip(1).Where(w => Vector2.Distance(w.Position, caster.Position) <= damageAura.GetRange && w.TeamNumber != caster.TeamNumber), w => NotEqual(w.Hp, w.GetBaseHp));
        }

        [Fact]
        public void SingleCleanupTest()
        {
            var allWarriors = new List<Warrior>();
            var factory = new ArcherFactory(allWarriors);
            var singleCleanup = new SingleCleanup(allWarriors);

            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            allWarriors.Add(factory.CreateWarrior(new Vector2(20, 5), 3));
            allWarriors.Add(factory.CreateWarrior(new Vector2(30, 5), 2));
            allWarriors.Add(factory.CreateWarrior(new Vector2(50, 5), 3));

            var caster = allWarriors.First();
            var target = allWarriors[1];


            foreach (var warrior in allWarriors)
            {
                warrior.Def -= 2;
            }

            All(allWarriors, w => NotEqual(w.Def, w.GetBaseDef));

            singleCleanup.UseSkill(caster);

            Equal(target.Def, target.GetBaseDef);
            }

        [Fact]
        public void DefDamageAuraSelfHealingTest()
        {
            var allWarriors = new List<Warrior>();
            var factory = new ArcherFactory(allWarriors);
            var  damageAuraSelfHealing = new DefDamageAuraSelfHealing(allWarriors);

            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            allWarriors.Add(factory.CreateWarrior(Vector2.One, 3));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            allWarriors.Add(factory.CreateWarrior(new Vector2(0, 5), 3));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 3));
            allWarriors.Add(factory.CreateWarrior(Vector2.Zero, 2));
            var caster = allWarriors.First();

            caster.Hp -= 20;

            NotEqual(caster.GetBaseHp, caster.Hp);
            All(allWarriors.Skip(1).Where(w => Vector2.Distance(w.Position, caster.Position) <= damageAuraSelfHealing.GetRange && w.TeamNumber != caster.TeamNumber), w => Equal(w.GetBaseHp, w.Hp));
            All(allWarriors.Skip(1).Where(w => Vector2.Distance(w.Position, caster.Position) <= damageAuraSelfHealing.GetRange && w.TeamNumber == caster.TeamNumber), w => Equal(w.GetBaseDef, w.Def));

            for (int i = 0; i < 40; i++)
            {
                damageAuraSelfHealing.UseSkill(caster);
            }

            All(allWarriors.Skip(1).Where(w => Vector2.Distance(w.Position, caster.Position) <= damageAuraSelfHealing.GetRange && w.TeamNumber != caster.TeamNumber), w => NotEqual(w.GetBaseHp, w.Hp));
            All(allWarriors.Skip(1).Where(w => Vector2.Distance(w.Position, caster.Position) <= damageAuraSelfHealing.GetRange && w.TeamNumber == caster.TeamNumber), w => NotEqual(w.GetBaseDef, w.Def));
            Equal(caster.GetBaseHp, caster.Hp);
        }


        [Fact]
        public void SelfHealTest()
        {
            var factory = new ArcherFactory();
            var selfHealing = new SelfHealing();

            var caster = factory.CreateWarrior(Vector2.Zero, 3);
            caster.Hp -= 20;

            NotEqual(caster.Hp, caster.GetBaseHp);
            for (int i = 0; i < 50; i++)
            {
                selfHealing.UseSkill(caster);
            }

            Equal(caster.Hp, caster.GetBaseHp);
        }

        [Fact]
        public void SelfCleanupTest()
        {
            var factory = new ArcherFactory();
            var selfCleanup = new SelfCleanup();

            var caster = factory.CreateWarrior(Vector2.Zero, 3);
            caster.AttackPower -= 5;
            caster.Def += 5;

            NotEqual(caster.AttackPower, caster.GetBaseAttackPower);
            NotEqual(caster.Def, caster.GetBaseDef);

            selfCleanup.UseSkill(caster);


            Equal(caster.AttackPower, caster.GetBaseAttackPower);
            Equal(caster.Def, caster.GetBaseDef);
        }

        [Theory]
        [InlineData(2, 4, 7)]
        [InlineData(0, 4, 1)]
        [InlineData(-3, 4, 0)]
        [InlineData(-2, 0, -3)]

        public void FactoryTest(float x, float y, int teamNumber)
        {
            var warriors = new List<Warrior>();

            var factories = new List<WarriorFactory>
            {
                new ArcherFactory(warriors), new SwordsmanFactory(warriors), new LanceKnightFactory(warriors)
            };

            foreach (var factory in factories)
            {
                warriors.Add(factory.CreateWarrior(new Vector2(x, y), teamNumber));
            }

            NotNull(warriors);
            NotNull(factories);

            All(warriors, NotNull);
            All(factories, NotNull);

            All(warriors, w => Equal(new Vector2(x, y),w.Position));
            if (teamNumber>=0)
            {
                All(warriors, w => Equal(teamNumber,w.TeamNumber));
            }
            else
            {
                All(warriors, w => Equal(0, w.TeamNumber));
            }
        }

        [Fact]
        public void BattleCleanTest()
        {
            var battle = new Battle();

            battle.AddWarrior<ArcherFactory>(Vector2.One, 4);
            battle.AddWarrior<ArcherFactory>(Vector2.One, 4);
            battle.AddWarrior<SwordsmanFactory>(Vector2.One, 4);
            battle.AddWarrior<ArcherFactory>(Vector2.One, 4);
            battle.AddWarrior<ArcherFactory>(Vector2.One, 4);

            NotEmpty(battle.Warriors);

            battle.Clear();

            Empty(battle.Warriors);
        }

        [Fact]
        public void BattleRemoveTest()
        {
            var battle = new Battle();

            battle.AddWarrior<ArcherFactory>(Vector2.One, 4);
            battle.AddWarrior<ArcherFactory>(Vector2.One, 4);
            battle.AddWarrior<SwordsmanFactory>(Vector2.One, 4);
            battle.AddWarrior<ArcherFactory>(Vector2.One, 4);
            battle.AddWarrior<ArcherFactory>(Vector2.One, 4);
            battle.AddWarrior<ArcherFactory>(Vector2.One, 4);
            battle.AddWarrior<ArcherFactory>(Vector2.One, 4);

            NotEmpty(battle.Warriors);

            var removed = battle.Warriors[3];
            NotNull(removed);

            battle.Remove(removed.Id);

            DoesNotContain(removed, battle.Warriors);
            NotNull(removed);
        }

        [Fact]
        public void BattleStartStopTest()
        {
            var battle = new Battle();

            battle.AddWarrior<ArcherFactory>(Vector2.One, 4);
            battle.AddWarrior<ArcherFactory>(Vector2.One, 4);
            battle.AddWarrior<SwordsmanFactory>(Vector2.One, 4);

            battle.Start();

            False(battle.IsStarted);

            battle.AddWarrior<ArcherFactory>(Vector2.One, 3);
            battle.AddWarrior<SwordsmanFactory>(Vector2.One, 3);
            battle.AddWarrior<SwordsmanFactory>(Vector2.One, 7);

            battle.Start();

            True(battle.IsStarted);

            battle.Stop();

            False(battle.IsStarted);
        }

        [Fact]
        public void BattleLogTest()
        {
            var battle = new Battle();

            battle.AddWarrior<ArcherFactory>(Vector2.One, 4);
            battle.AddWarrior<ArcherFactory>(Vector2.One, 4);
            battle.AddWarrior<SwordsmanFactory>(Vector2.One, 4);

            Empty(battle.GetLog());

            battle.Start();

            Thread.Sleep(1000);
            NotEmpty(battle.GetLog());
        }
    }
}
