namespace ArmyBattle.Warriors
{
    using System;
    using System.Collections.Generic;
    using Skills;
    using WarriorsDraw;
    using Abstraction;
    using System.Numerics;
    public sealed class Swordsman : Warrior
    {
        public override DrawWarrior DrawWarrior { get; protected set; }
        public override Vector2 Position { get; set; }
        public override int CommandNumber { get; set; }
        public override int Hp { get; set; }
        public override int Def { get; set; }
        public override int AttackPower { get; set; }
        public override int AttackRange { get; set; }
        public override ISkill Skill { get; protected set; }
        public override List<Type> DominanceWarriors { get; protected set; }
        public override List<Type> SuppressionWarriors { get; protected set; }

        public Swordsman()
        {
            AttackRange = 1;
            AttackPower = 10;
            Def = 5;
            Hp = 200;

            Skill = new DefAura();
            DrawWarrior = new SwordsmanDraw(Hp);

            DominanceWarriors = new List<Type>()
            {

            };
            SuppressionWarriors = new List<Type>()
            {

            };
        }
        public Swordsman(Vector2 position, int commandNumber):this()
        {
            CommandNumber = commandNumber;
            Position = position;
        }
    }
}
