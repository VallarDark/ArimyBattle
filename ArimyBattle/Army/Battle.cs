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
    using System.Text;

    /// <summary>
    /// Calculates all parameters of warriors every n seconds.
    /// Allows you to add new instances of warriors using their factories.
    /// Stops the fight if 1 of the teams wins.
    /// Allows user to start and stop a battle, remove 1 or all warriors from the battle.
    /// Logs messages from warriors.
    /// </summary>
    public class Battle
    {
        private const int BaseCounterC = 15;

        private bool _isStarted;
        private readonly Timer _timer;
        private StringBuilder _stringBuilder;

        private readonly List<Warrior> _warriors;
        private readonly List<WarriorFactory> _factories;

        private string _logger = "";


        private int _counter = BaseCounterC;

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
            _timer = new Timer { Interval = 150, Enabled = true };
            _timer.Elapsed += StartRound;

            _warriors = new List<Warrior>();
            _factories = new List<WarriorFactory>();
            UnitInitializer();
        }


        /// <summary>
        /// Creates a predefined war instance and adds it to the arena
        /// </summary>
        /// <typeparam name="T"> Warrior factory (ArcherFactory, SwordsmanFactory..) </typeparam>
        /// <param name="position"> spawn position of warrior</param>
        /// <param name="teamNumber">number of team (identifies allies)</param>
        /// <returns>link to the created warrior</returns>
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

            if (!HasOtherTeams())
            {
                try
                {
                    var first = _warriors.FirstOrDefault();
                    _logger  = $"!!!!!! Win {first?.TeamNumber} team !!!!!!!!!";
                    Stop();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }


            Parallel.For(0, _warriors.Count, index =>
            {
                if (index >= 0 && index < _warriors.Count)
                {
                    try
                    {
                        var warrior = _warriors[index];
                        if (warrior != null)
                        {
                            warrior.Action();

                            if (_counter == 0)
                            {
                                try
                                {
                                    var log = warrior.GetLog() + "\n_______________\n\n";
                                    _stringBuilder = new StringBuilder(_logger, _logger.Length + log.Length + 5);
                                    _stringBuilder.Insert(_stringBuilder.Length, log);
                                    _logger = _stringBuilder.ToString();
                                }
                                catch (Exception exception)
                                {
                                    Console.WriteLine(exception);
                                }
                            }
                            else if (_counter == 1)
                            {
                                _logger = "";
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                }

            });
            _counter--;
            if (_counter < 0)
            {
                _counter = BaseCounterC;
            }
        }

        private bool HasOtherTeams()
        {
            var first = _warriors.FirstOrDefault();
            if (first == null)
            {
                return false;
            }
            return _warriors.Any(w => w.TeamNumber != first.TeamNumber);
        }

        public void Start()
        {
            if (HasOtherTeams())
            {
                if (!_isStarted)
                {
                    _isStarted = true;
                }
                _timer.Start();
            }
        }

        /// <summary>
        /// Stops the fight timer.
        /// </summary>
        public void Stop()
        {
            _timer.Stop();
            _isStarted = false;
        }

        /// <summary>
        /// Stops the combat timer and removes all warriors from the battle.
        /// </summary>
        public void Clear()
        {
            _timer.Stop();
            _warriors.Clear();
        }

        /// <summary>
        /// Removes a warrior from combat, if one exists, without stopping the timer.
        /// </summary>
        /// <param name="id">Warrior id</param>
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
