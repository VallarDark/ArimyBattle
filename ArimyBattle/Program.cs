

using System.Numerics;

namespace ArmyBattle
{
    using Army;
    using Factories;
    class Program
    {
        static void Main()
        {
            var battle = new Battle();
            var warrior = battle.AddWarrior<ArcherFactory>(new Vector2(2, 5), 2);
            var warrior2 = battle.AddWarrior<SwordsmanFactory>(new Vector2(2, 5), 2);

        }
    }
}
