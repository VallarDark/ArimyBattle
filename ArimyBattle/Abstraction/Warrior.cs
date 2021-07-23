using System.Linq;

namespace ArmyBattle.Abstraction
{
    using System.Collections.Generic;
    using System.Numerics;
    using System;
    public abstract class Warrior : IWarrior
    {

        protected Random Random;

        protected bool HasAction;
        protected bool IsAlive;
        protected int AttackCounter;    // timer to reset action       
        protected int DieCounter;       // rounds count before clear dead warrior

        protected readonly int BaseHp;
        protected readonly int BaseDef;
        protected readonly int BaseAttackPower;
        protected readonly int BaseAttackRange;
        protected readonly int BaseAttackResetTime;
        protected readonly int BaseTeamNumber;

        protected int InnerHp;
        protected int InnerDef;
        protected int InnerAttackPower;
        protected int InnerAttackRange;
        protected int InnerAttackResetTime;
        protected int InnerTeamNumber;

        public int TeamNumber
        {
            get => InnerTeamNumber;
            set
            {
                if (value>=0)
                {
                    InnerTeamNumber = value;
                }
            }
        }
        public int Hp
        {
            get => InnerHp;
            set
            {
                if (value >= 0 && value<=BaseHp)
                {
                    InnerHp = value;
                }
            }
        }
        public int Def {
            get => InnerDef;
            set
            {
                if (value >= 0)
                {
                    InnerDef = value;
                }
            }
        }
        public int AttackPower {
            get => InnerAttackPower;
            set
            {
                if (value >= 0)
                {
                    InnerAttackPower = value;
                }
            }
        }
        public int AttackRange {
            get => InnerAttackRange;
            set
            {
                if (value >= 0)
                {
                    InnerAttackRange = value;
                }
            }
        }
        public int AttackResetTime {
            get => InnerAttackResetTime;
            set
            {
                if (value >= 0)
                {
                    InnerAttackResetTime = value;
                }
            }
        }

        public Vector2 Position { get; set; }

        public List<Warrior> AllUnits { get; protected set; }
        public ISkill Skill { get; protected set; }
        public List<Type> DominanceWarriors { get; protected set; }
        public List<Type> SuppressionWarriors { get; protected set; }

        public void ResetHp()
        {
            Hp = BaseHp;
        }

        protected void ResetDef()
        {
            Def = BaseDef;
        }

        protected void ResetAttackPower()
        {
            AttackPower = BaseAttackPower;
        }

        protected void ResetAttackRange()
        {
            AttackRange = BaseAttackRange;
        }

        protected void ResetAttackResetTime()
        {
            AttackResetTime = BaseAttackResetTime;
        }

        protected void ResetCommandNumber()
        {
            TeamNumber = BaseTeamNumber;
        }

        public void ResetCharacteristics()
        {
            ResetDef();
            ResetAttackPower();
            ResetAttackRange();
            ResetAttackResetTime();
            ResetCommandNumber();
        }

        protected Warrior(List<Warrior> allUnits, Vector2 position, int teamNumber, int hp, int def, int attackPower, int attackRange, int attackResetTime, ISkill skill, List<Type> dominanceWarriors, List<Type> suppressionWarriors)
        {
            BaseTeamNumber = teamNumber;
            BaseHp = hp;
            BaseDef = def;
            BaseAttackPower = attackPower;
            BaseAttackRange = attackRange;
            BaseAttackResetTime = attackResetTime;

            IsAlive = true;
            DieCounter = 10;

            AllUnits = allUnits;
            Position = position;
            TeamNumber = teamNumber;
            Hp = hp;
            Def = def;
            AttackPower = attackPower;
            AttackRange = attackRange;
            AttackResetTime = attackResetTime;
            Skill = skill;
            DominanceWarriors = dominanceWarriors;
            SuppressionWarriors = suppressionWarriors;

        }

        protected Warrior(Vector2 position, int teamNumber)
        {
            IsAlive = true;

            Position = position;
            TeamNumber = teamNumber;

            BaseTeamNumber = teamNumber;
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
            return AllUnits.FirstOrDefault(t => Vector2.Distance(Position, t.Position) <= AttackRange && t.TeamNumber != TeamNumber);
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
            if (IsAlive)
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
                    if (AttackCounter <= 0)
                    {
                        AttackCounter = AttackResetTime;
                        HasAction = false;
                    }

                    AttackCounter--;
                }
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
                IsAlive = false;
            }
        }

        public abstract Warrior GetInstance(Vector2 position, int teamNumber);

    }
}
