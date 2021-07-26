


using System.Text;

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

        private bool _isStarted;
        private readonly Timer _timer;
        private StringBuilder _stringBuilder;

        private readonly List<Warrior> _warriors;
        private readonly List<WarriorFactory> _factories;

        private string _logger = "";
        private int _counter = 10;

        public bool IsStarted => _isStarted;
        public List<Warrior> Warriors => _warriors;
        public void UnitInitializer()
        {
            _factories.Add(new SwordsmanFactory(_warriors));
            _factories.Add(new ArcherFactory(_warriors));
            _factories.Add(new LanceKnightFactory(_warriors));
        }

        public string GetLog() => _logger;
        public Battle()
        {
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


            for (var index = 0; index < _warriors.Count; index++)
            {
                var warrior = _warriors[index];
                if (warrior != null)
                {
                    Task.Run(() =>
                {
                    warrior.Action();
                    if (_counter==0)
                    {
                        var log= warrior.GetLog()+ "\n_______________\n\n";
                        _stringBuilder = new StringBuilder(_logger, _logger.Length + log.Length);
                        _stringBuilder.Insert(_stringBuilder.Length, log);
                        _logger = _stringBuilder.ToString();
                        _counter = 15;
                    }

                    _counter--;
                });
                }

            }

            //Parallel.For(0, _warriors.Count, index =>
            // {
            //     if (index < _warriors.Count)
            //     {
            //         _warriors[index].Action();
            //         lock (_locker)
            //         {
            //             _logger += _warriors[index].GetLog();
            //         }
            //     }
            // });
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
