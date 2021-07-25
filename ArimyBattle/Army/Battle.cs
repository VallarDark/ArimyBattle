


namespace ArmyBattle.Army
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System;
    using System.Threading.Tasks;
    using System.Timers;
    using Abstraction;
    using Factories;

    public class Battle
    {
        private int _round;
        private bool _isStarted;
        private readonly Timer _timer;

        private readonly List<Warrior> _warriors;
        private readonly List<WarriorFactory> _factories;

        public bool IsStarted => _isStarted;
        public void UnitInitializer()
        {
            _factories.Add(new SwordsmanFactory(_warriors));
            _factories.Add(new ArcherFactory(_warriors));
            _factories.Add(new LanceKnightFactory(_warriors));
        }

        public Battle()
        {
            _round = 1;
            _timer = new Timer();
            _warriors = new List<Warrior>();
            _factories = new List<WarriorFactory>();
            UnitInitializer();
        }

        public Warrior AddWarrior<T>(Vector2 position, int teamNumber) where T : WarriorFactory
        {
            var newWarrior = _factories.FirstOrDefault(f => f.GetType() == typeof(T))?.CreateWarrior(position, teamNumber);
            if (newWarrior != null)
            {
                _warriors.Add(newWarrior);
            }

            return newWarrior;
        }

        private void StartRound(object s, EventArgs e)
        {

            if (HasOtherTeams())
            {
                Warrior first = null;
                foreach (var warrior in _warriors)
                {
                    first = warrior;
                    break;
                }

                if (first != null)
                {
                    Console.WriteLine($"!!!!!! Win {first.TeamNumber} team !!!!!!!!!");
                }

                Stop();
            }

            Parallel.For(0, _warriors.Count, index =>
             {
                 if (index < _warriors.Count)
                 {
                     _warriors[index].Action();
                 }

             });

            Console.WriteLine($"{_round++} round");
        }

        private bool HasOtherTeams()
        {
            var first = _warriors.FirstOrDefault();
            if (first == null)
            {
                return true;
            }
            return _warriors.All(w => w.TeamNumber == first.TeamNumber);
        }

        public void Start()
        {
            if (!_isStarted)
            {
                _isStarted = true;
            }
            _timer.Interval = 500;
            _timer.Enabled = true;
            _timer.Elapsed += StartRound;
            _timer.Start();
        }
        public void Stop()
        {
            _timer.Stop();
            _isStarted = false;
        }
        public void Clear()
        {
            _timer.Stop();
            _warriors.Clear();
        }
        public void Remove(int id)
        {
            var warrior = _warriors.FirstOrDefault(w => w.Id == id);
            if (warrior != null)
            {
                _timer.Stop();
                _warriors.Remove(warrior);
                _timer.Start();
            }
        }
    }
}
