namespace ArmyBattle.Army
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Timers;
    using Abstraction;
    using Skills;
    using Warriors;
    public class Battle
    {
        private readonly List<Warrior> _warriors;
        private readonly List<Warrior> _prototypes;
        private readonly Timer _timer;

        public void UnitInitializer()
        {
            _prototypes.Add(
                new Swordsman(

                    allUnits: _warriors,
                    position: new Vector2(0, 0),
                    teamNumber: 0,
                    hp: 200,
                    def: 5,
                    attackPower: 10,
                    attackRange: 1,
                    attackResetTime: 3,
                    skill: new DefAura(_warriors),
                    dominanceWarriors: new List<Type>(),
                    suppressionWarriors: new List<Type>()));
        }


        public Battle()
        {
            _timer = new Timer();
            _warriors = new List<Warrior>();
            _prototypes = new List<Warrior>();
            UnitInitializer();
        }

        public void AddWarrior(Warrior newWarrior)
        {
            var sameType = _prototypes.FirstOrDefault(p => p.GetType() == newWarrior.GetType());
            if (sameType != null)
            {
                var result = sameType.GetInstance(newWarrior.Position, newWarrior.TeamNumber);
                _warriors.Add(result);
            }
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
