using System.Linq;

namespace ArmyBattle.Abstraction
{
    using System.Collections.Generic;
    using System.Numerics;
    using System;
    public abstract class Warrior : IWarrior
    {
        protected bool HasAction;
        protected Random Random;
        protected int AttackCounter;
        public List<Warrior> AllUnits { get; set; }
        public Vector2 Position { get; set; }
        public int CommandNumber { get; set; }
        public int Hp { get; set; }
        public int Def { get; set; }
        public int AttackPower { get; set; }
        public int AttackRange { get; set; }
        public int AttackResetTime { get; set; }
        public ISkill Skill { get; protected set; }
        public List<Type> DominanceWarriors { get; protected set; }
        public List<Type> SuppressionWarriors { get; protected set; }

        protected Warrior(List<Warrior> allUnits, Vector2 position, int commandNumber, int hp, int def, int attackPower, int attackRange, int attackResetTime, ISkill skill, List<Type> dominanceWarriors, List<Type> suppressionWarriors)
        {
            AllUnits = allUnits;
            Position = position;
            CommandNumber = commandNumber;
            Hp = hp;
            Def = def;
            AttackPower = attackPower;
            AttackRange = attackRange;
            AttackResetTime = attackResetTime;
            Skill = skill;
            DominanceWarriors = dominanceWarriors;
            SuppressionWarriors = suppressionWarriors;
        }

        protected Warrior(Vector2 position, int commandNumber)
        {
            Position = position;
            CommandNumber = commandNumber;
        }

        protected virtual int CalculateAttackPower(Warrior target)
        {
            var currentAttackPower = AttackPower;
            if (DominanceWarriors.Contains(target.GetType()))
            {
                currentAttackPower = (int)(currentAttackPower * 0.25f);
            }
            else if (SuppressionWarriors.Contains(target.GetType()))
            {
                currentAttackPower = (int)(currentAttackPower / 0.25f);
            }

            return currentAttackPower;
        }

        protected virtual Warrior GetTarget()
        {
            return AllUnits.FirstOrDefault(t => Vector2.Distance(Position, t.Position) <= AttackRange && t.CommandNumber != CommandNumber);
        }

        public virtual void Attack(Warrior target)
        {
            var damage = CalculateAttackPower(target) - target.Def;
            target.Hp -= damage;
        }


        public void UseSkill(params Warrior[] targets)
        {
            Skill.UseSkill(this);
        }


        public virtual void Run(Vector2 direction)
        {
            Position += direction;
        }

        public void Action()
        {
            if (!HasAction)
            {
                HpCheck();
                var target = GetTarget();
                if (target != null)
                {
                    Attack(target);
                    HasAction = true;
                }
                else
                {
                    var newPosition = new Vector2(Random.Next(-1, 1), Random.Next(-1, 1));
                    var neighbor = AllUnits.FirstOrDefault(u =>
                        Vector2.Distance(Position + newPosition, u.Position) <= 0.5 && u != this);
                    if (neighbor != null)
                    {
                        Rearrangement(neighbor);
                    }
                    else
                    {
                        Position += newPosition;
                    }
                    HasAction = false;
                }
            }
            else
            {
                if (AttackCounter<=0)
                {
                    AttackCounter = AttackResetTime;
                    HasAction = false;
                }

                AttackCounter--;
            }
        }

        public virtual void Rearrangement(Warrior target)
        {
            var temp = Position;
            Position = target.Position;
            target.Position = temp;
        }


        public virtual void HpCheck()
        {
            if (Hp <= 0)
            {
                AllUnits.Remove(this);
            }
        }

        public abstract Warrior GetInstance(Vector2 position, int commandNumber);

    }
}
