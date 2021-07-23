namespace ArmyBattle.Army
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Timers;
    using Abstraction;
    using Factories;

    public class Battle
    {
        private readonly List<Warrior> _warriors;
        private readonly List<WarriorFactory> _factories;
        private readonly Timer _timer;

        public void UnitInitializer()
        {
            _factories.Add(new SwordsmanFactory(_warriors));
            _factories.Add(new ArcherFactory(_warriors));
            _factories.Add(new LanceKnightFactory(_warriors));
        }

        public Battle()
        {
            _timer = new Timer();
            _warriors = new List<Warrior>();
            _factories = new List<WarriorFactory>();
            UnitInitializer();
        }

        public Warrior AddWarrior<T>(Vector2 position ,int teamNumber) where T : WarriorFactory
        {
            return _factories.FirstOrDefault(f => f.GetType() == typeof(T))?.CreateWarrior(position, teamNumber);
        }

        private void StartRound(object sender, ElapsedEventArgs e)
        {
            foreach (var warrior in _warriors)
            {
                warrior.Action();
            }
        }

        public void Start()
        {
            _timer.Interval = 200;
            _timer.Elapsed += StartRound;
            _timer.Start();
        }
        public void Stop()
        {
            _timer.Stop();
        }
    }
}
