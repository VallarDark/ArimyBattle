namespace ArmyBattle.Abstraction
{
    using System.Collections.Generic;
    using System.Numerics;
    using System;
    public abstract class Warrior : IWarrior,IDrawable
    {

        public abstract DrawWarrior DrawWarrior { get; protected set; }
        public abstract Vector2 Position { get; set; }
        public abstract int CommandNumber { get; set; }
        public abstract int Hp { get;  set; }
        public abstract int Def { get;  set; }
        public abstract int AttackPower { get; set; }
        public abstract int AttackRange { get; set; }
        public abstract ISkill Skill { get; protected set; }
        public abstract List<Type> DominanceWarriors { get; protected set; }
        public abstract List<Type> SuppressionWarriors { get; protected set; }

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

        public virtual void Attack(Warrior target)
        {
            var damage = CalculateAttackPower(target)-target.Def;
            target.Hp -= damage;
        }

        public void UseSkill(params Warrior[] targets)
        {
            Skill.UseSkill(this,targets);
        }


        public virtual void Run(Vector2 direction)
        {
            Position+=direction; 
        }

        public virtual void Rearrangement(Warrior target)
        {
            var temp = Position;
            Position = target.Position;
            target.Position = temp;
        }


        public virtual void HpCheck()
        {
            if (Hp<=0)
            {
                DrawWarrior.Die();
            }
        }

        public void Draw()
        {
            DrawWarrior.Draw(Hp);
        }
    }
}
