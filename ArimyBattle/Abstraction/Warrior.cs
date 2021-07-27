namespace ArmyBattle.Abstraction
{
    using System.Collections.Generic;
    using System.Numerics;
    using System;
    using System.Text;
    using System.Linq;
    public abstract class Warrior : IWarrior
    {
        private const string AttackC = "_____Attack_____";
        private const string FromToC = "from____Run____to";
        private const string DeadC = "____Dead____";

        public delegate void DeadDelegate();
        /// <summary>
        /// Execute the passed code when the warrior dies.
        /// </summary>
        public event DeadDelegate Dead;

        private StringBuilder _stringBuilder;
        private static int _counter = 1;
        private string _logger = "";

        protected Random Random;

        protected bool HasAction;
        protected bool IsAlive => Hp > 0;
        protected int AttackCounter;        // timer to reset action       

        protected int TravelDistance;

        protected readonly int BaseHp;
        protected readonly int BaseDef;
        protected readonly int BaseAttackPower;
        protected readonly int BaseAttackRange;
        protected readonly int BaseAttackResetTime;
        protected readonly int BaseTeamNumber;

        public  int GetBaseHp=>BaseHp;
        public  int GetBaseDef=>BaseDef;
        public  int GetBaseAttackPower=>BaseAttackPower;
        public  int GetBaseAttackRange=>BaseAttackRange;
        public  int GetBaseAttackResetTime=>BaseAttackResetTime;
        public  int GetBaseTeamNumber=>BaseTeamNumber;

        protected int InnerHp;
        protected int InnerDef;
        protected int InnerAttackPower;
        protected int InnerAttackRange;
        protected int InnerAttackResetTime;
        protected int InnerTeamNumber;

        public int Id { get; private set; }
        public int TeamNumber
        {
            get => InnerTeamNumber;
            set
            {
                if (value >= 0)
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
                if (value <= BaseHp)
                {
                    if (value < 0)
                    {
                        value = 0;
                    }
                    InnerHp = value;
                }
            }
        }
        public int Def
        {
            get => InnerDef;
            set
            {
                if (value >= 0)
                {
                    InnerDef = value;
                }
            }
        }
        public int AttackPower
        {
            get => InnerAttackPower;
            set
            {
                if (value >= 0)
                {
                    InnerAttackPower = value;
                }
            }
        }
        public int AttackRange
        {
            get => InnerAttackRange;
            set
            {
                if (value >= 0)
                {
                    InnerAttackRange = value;
                }
            }
        }
        public int AttackResetTime
        {
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
        public string TroopsType => GetType().Name;
        public List<Warrior> AllUnits { get; protected set; }
        public ISkill Skill { get; protected set; }
        public List<Type> DominanceWarriors { get; protected set; }
        public List<Type> SuppressionWarriors { get; protected set; }

        protected void ResetHp()
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

        /// <summary>
        /// Clear all negative effects. Can be used in skill.
        /// </summary>
        public void ResetCharacteristics()
        {
            ResetDef();
            ResetAttackPower();
            ResetAttackRange();
            ResetAttackResetTime();
            ResetCommandNumber();
        }

        protected Warrior(List<Warrior> allUnits, Vector2 position, int teamNumber,
            int hp, int def, int attackPower, int attackRange, int attackResetTime,
            ISkill skill, List<Type> dominanceWarriors, List<Type> suppressionWarriors)
        {
            Id = _counter;
            BaseTeamNumber = teamNumber;
            BaseHp = hp;
            BaseDef = def;
            BaseAttackPower = attackPower;
            BaseAttackRange = attackRange;
            BaseAttackResetTime = attackResetTime;

            TravelDistance = 3;

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
            _counter++;
        }

        protected virtual int CalculateAttackPower(Warrior target)
        {
            var currentAttackPower = AttackPower;
            if (DominanceWarriors.Contains(target.GetType()))
            {
                currentAttackPower = (int)(currentAttackPower / 0.25f);
            }
            else if (SuppressionWarriors.Contains(target.GetType()))
            {
                currentAttackPower = (int)(currentAttackPower * 0.25f);
            }

            return currentAttackPower;
        }

        protected virtual Warrior GetTarget()
        {
            return AllUnits.FirstOrDefault(t => Vector2.Distance(Position, t.Position) <= AttackRange && t.TeamNumber != TeamNumber);
        }

        protected virtual Warrior GetPotentialTarget()
        {
            return AllUnits.OrderBy(t => Vector2.Distance(Position, t.Position)).FirstOrDefault(t => t.TeamNumber != TeamNumber);
        }

        protected void ShortLog(Warrior warrior)
        {
            try
            {

                var tempLog = $"\n id {warrior.Id} \n" +
                                          $" type {warrior.TroopsType} \n" +
                                          $" position {warrior.Position} \n" +
                                          $" hp {warrior.Hp} \n\n";

                _stringBuilder = new StringBuilder(_logger, _logger.Length + tempLog.Length + 5);
                _stringBuilder.Insert(_stringBuilder.Length, tempLog);
                _logger = _stringBuilder.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected void FullLog(Warrior caster)
        {
            try
            {
                _stringBuilder = new StringBuilder(_logger, _logger.Length + caster.ToString().Length + 5);
                _stringBuilder.Insert(_stringBuilder.Length, caster);
                _logger = _stringBuilder.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Reduces the enemy's HP if he is in the attack zone.
        /// </summary>
        /// <param name="target"> Other warrior as target for attack. </param>
        public virtual void Attack(Warrior target)
        {
            if (Vector2.Distance(Position, target.Position) <= AttackRange)
            {
                try
                {
                    var damage = CalculateAttackPower(target) - target.Def;
                    target.Hp -= damage;
                    ShortLog(this);
                    _stringBuilder = new StringBuilder(_logger, _logger.Length + AttackC.Length + 5);
                    _stringBuilder.Insert(_stringBuilder.Length, AttackC);
                    _logger = _stringBuilder.ToString();

                    ShortLog(target);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void UseSkill()
        {
            Skill.UseSkill(this);
        }

        /// <summary>
        /// Runs in a straight line with the specified end point for the maximum travel distance.
        /// </summary>
        /// <param name="direction"> Direction of travel</param>
        public virtual void Run(Vector2 direction)
        {
            try
            {
                ShortLog(this);
                _stringBuilder = new StringBuilder(_logger, _logger.Length + FromToC.Length + 5);
                _stringBuilder.Insert(_stringBuilder.Length, FromToC);
                _logger = _stringBuilder.ToString();

                Position += direction;

                ShortLog(this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Challenge the warrior's inner logic. Includes attack, movement and challenge ability.
        /// </summary>
        public void Action()
        {
            if (IsAlive)
            {
                if (!HasAction)
                {
                    var target = GetTarget();
                    if (target != null)
                    {
                        UseSkill();
                        Attack(target);
                        HasAction = true;
                    }
                    else
                    {
                        var potentialTarget = GetPotentialTarget();
                        if (potentialTarget == null)
                        {
                            return;
                        }
                        var targetPosition = potentialTarget.Position;

                        var delta = targetPosition - Position;
                        if (delta.X < -TravelDistance)
                        {
                            delta.X = -TravelDistance;
                        }
                        else if (delta.X > TravelDistance)
                        {
                            delta.X = TravelDistance;
                        }

                        if (delta.Y < -TravelDistance)
                        {
                            delta.Y = -TravelDistance;
                        }
                        else if (delta.Y > TravelDistance)
                        {
                            delta.Y = TravelDistance;
                        }

                        Run(delta);

                        HasAction = false;
                    }
                }
                else
                {
                    if (AttackCounter <= 0)
                    {
                        AttackCounter = AttackResetTime;
                        HasAction = false;
                        ResetCharacteristics();
                    }

                    AttackCounter--;
                }

            }
            else
            {
                Die();
            }
        }

        /// <summary>
        /// The log contains messages about all actions of the warrior.
        /// </summary>
        /// <returns> Event log up to the current moment</returns>
        public string GetLog() => _logger;

        /// <summary>
        /// Creates a clone of a warrior based on the prototype with the specified position and team number.
        /// </summary>
        /// <param name="prototype">Warrior prototype to copy</param>
        /// <param name="position">New position</param>
        /// <param name="teamNumber">New team number</param>
        /// <returns>A new warrior with a copy of the characteristics and skills of the prototype</returns>
        public abstract Warrior GetInstance(Warrior prototype, Vector2 position, int teamNumber);

        public void Die()
        {
            try
            {
                Dead?.Invoke();
                AllUnits.Remove(this);
                _stringBuilder = new StringBuilder(_logger, _logger.Length + DeadC.Length + 5);
                _stringBuilder.Insert(_stringBuilder.Length, DeadC);
                _logger = _stringBuilder.ToString();

                ShortLog(this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public override string ToString()
        {
            return $"\n\tid: {Id} \n" +
                   $"\ttype: {TroopsType} \n" +
                   $"\thp: {Hp} \n" +
                   $"\tdef: {Def}\n" +
                   $"\tposition: {Position.X},{Position.Y} \n" +
                   $"\tteam number: {TeamNumber} \n" +
                   $"\tattack power: {AttackPower} \n" +
                   $"\tattack range: {AttackRange} \n" +
                   $"\tattack reset time: {AttackResetTime} \n\n";
        }
    }
}
