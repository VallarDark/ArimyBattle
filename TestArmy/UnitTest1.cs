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
                dominanceWarriors: new List<Type>()
                {

                },
                suppressionWarriors: new List<Type>()
                {

                }
            );

            // Act
            var testWarrior = battle.AddWarrior<SwordsmanFactory>(position, teamNumber);

            // Assert
            Assert.Equal(resultWarrior.GetType(), testWarrior.GetType());
            Assert.Equal(resultWarrior.TeamNumber, testWarrior.TeamNumber);
            Assert.Equal(resultWarrior.Position, testWarrior.Position);
        }
    }
}