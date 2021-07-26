using System;

namespace BattleView
{
    using System.Numerics;
    using ArmyBattle.Factories;
    using System.Windows;
    using ArmyBattle.Army;
    using System.Timers;
    using Renderers;
    using System.Threading.Tasks;

    /// <summary>
    /// A simple user interface with the ability to interactively add a variety
    /// of units to an arbitrary position, determine their team affiliation.
    /// Remove a limited unit or clear the entire field. Stop and start combat are also available.
    /// </summary>
    public partial class MainWindow
    {
        private const string TeamErrorC = "\nYou can't start battle less then 2 other teams";
        private const int BaseCounterC = 15;

        private readonly Battle _battle;
        private readonly Renderer _renderer;
        private readonly Timer _renderTime;

        private int _counter = BaseCounterC;
        private int _stopCounter = BaseCounterC;
        public MainWindow()
        {
            InitializeComponent();
            _battle = new Battle();
            _renderer = new Renderer(_battle.Warriors, BattleCanvas);

            _renderTime = new Timer { Interval = 150, Enabled = false };
            _renderTime.Elapsed += RenderTimeElapsed;
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selected = Type.SelectedIndex;
            if (selected == -1)
            {
                selected = 0;
            }


            if (float.TryParse(PositionX.Text, out var positionX) && float.TryParse(PositionY.Text, out var positionY) &&
                int.TryParse(TeamNumber.Text, out var teamNumber))
            {
                if (teamNumber < 1)
                {
                    teamNumber = 1;
                }
                else if (teamNumber > 9)
                {
                    teamNumber = 9;
                }

                if (selected == 0)
                {
                    _battle.AddWarrior<SwordsmanFactory>(new Vector2(positionX, positionY), teamNumber);
                }
                else if (selected == 1)
                {
                    _battle.AddWarrior<LanceKnightFactory>(new Vector2(positionX, positionY), teamNumber);
                }
                else if (selected == 2)
                {
                    _battle.AddWarrior<ArcherFactory>(new Vector2(positionX, positionY), teamNumber);
                }

                Task.Run(() => { _renderer.Render(); });
            }
        }

        private void RenderTimeElapsed(object sender, ElapsedEventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    if (!_battle.IsStarted)
                    {
                        if (_stopCounter==0)
                        {
                            _renderTime.Stop();
                            _stopCounter = BaseCounterC;
                        }
                        
                        _stopCounter--;
                    }
                    _renderer.Render();
                    if (_counter == 0)
                    {
                        Dispatcher.Invoke(() => { LogArea.Text = _battle.GetLog(); });
                        _counter = BaseCounterC;
                    }
                    _counter--;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            });
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            _battle.Start();
            if (_battle.IsStarted)
            {
                _renderTime.Start();
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    LogArea.Text = TeamErrorC;
                });
            }
        }

        private void StopButton_OnClick(object sender, RoutedEventArgs e)
        {
            _battle.Stop();
            _renderTime.Stop();
        }

        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            _battle.Clear();
            _renderTime.Stop();
            _renderer.Clear();
        }

        private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(RemoveId.Text, out var removeId))
            {
                _renderer.Remove(removeId);
                _battle.Remove(removeId);
            }
        }
    }
}
