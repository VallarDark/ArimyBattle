namespace ArmyBattle.Abstraction
{
    using System.Collections.Generic;
    using System.Numerics;

    public abstract class WarriorFactory
    {
        protected Warrior Prototype;
        protected List<Warrior> AllWarriors;

        protected WarriorFactory(List<Warrior> allWarriors)
        {
            AllWarriors = allWarriors;
        }
        protected abstract Warrior SetPrototype();
        public Warrior CreateWarrior(Vector2 position,int teamNumber)
        {
            Prototype ??= SetPrototype();

            return Prototype.GetInstance(Prototype,position, teamNumber);
        }

    }
}
