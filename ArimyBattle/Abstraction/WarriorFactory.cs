namespace ArmyBattle.Abstraction
{
    using System.Collections.Generic;
    using System.Numerics;

    /// <summary>
    /// Creates a clone of a warrior based on the base prototype.
    /// </summary>
    public abstract class WarriorFactory
    {
        protected Warrior Prototype;
        protected List<Warrior> AllWarriors;

        protected WarriorFactory(List<Warrior> allWarriors=null)
        {
            AllWarriors = allWarriors??new List<Warrior>();
        }
        protected abstract Warrior SetPrototype();

        /// <summary>
        /// Creates a clone of a warrior based on the base prototype with the specified position and team number.
        /// </summary>
        /// <param name="position">New position</param>
        /// <param name="teamNumber">New team number</param>
        /// <returns>A new warrior with a copy of the characteristics and skills of the base prototype</returns>
        public Warrior CreateWarrior(Vector2 position,int teamNumber)
        {
            Prototype ??= SetPrototype();

            return Prototype.GetInstance(Prototype,position, teamNumber);
        }

    }
}
