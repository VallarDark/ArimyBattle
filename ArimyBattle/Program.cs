namespace ArmyBattle
{
    using System.Numerics;
    using Army;
    using Factories;
    using System.Threading;
    class Program
    {
        static void Main()
        {
            var battle = new Battle();
            var warrior = battle.AddWarrior<ArcherFactory>(new Vector2(2, 5), 2);
            var warrior2 = battle.AddWarrior<SwordsmanFactory>(new Vector2(8, 5), 3);
            var warrior3 = battle.AddWarrior<LanceKnightFactory>(new Vector2(9, -5), 5);

            battle.Start();

            while (battle.IsStarted)
            {
                
            }


        }
    }
}
