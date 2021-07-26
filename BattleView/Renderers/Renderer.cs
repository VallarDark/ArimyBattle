using System.Threading;
using System.Threading.Tasks;

namespace BattleView.Renderers
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using ArmyBattle.Abstraction;
    public class Renderer
    {
        private readonly Dictionary<Warrior, Ellipse> _renderedWarriors;
        private readonly List<Warrior> _warriors;
        private readonly Canvas _canvas;
        private readonly int _radius;
        private readonly Dictionary<int, SolidColorBrush> _teamColors;
        private readonly Dictionary<string, SolidColorBrush> _typeColors;
        public Renderer(List<Warrior> warriors, Canvas canvas, int radius = 15)
        {
            _renderedWarriors = new Dictionary<Warrior, Ellipse>();
            _warriors = warriors;
            _canvas = canvas;
            _radius = radius;
            _teamColors = new Dictionary<int, SolidColorBrush>()
            {
                {1,Brushes.Black },
                {2,Brushes.BurlyWood },
                {3,Brushes.LightGray },
                {4,Brushes.Tomato },
                {5,Brushes.Yellow },
                {6,Brushes.Green },
                {7,Brushes.Blue },
                {8,Brushes.Fuchsia },
                {9,Brushes.DarkRed },
            };
            _typeColors = new Dictionary<string, SolidColorBrush>()
            {
                {"Archer",Brushes.DarkGreen},
                {"LanceKnight",Brushes.DarkSlateGray},
                {"Swordsman",Brushes.Gray}
            };
        }
        public void Render()
        {
            for (var index = 0; index < _warriors.Count; index++)
            {
                var warrior = _warriors[index];
                if (warrior!=null)
                {
                     Draw(warrior);
                }
            }
        }

        private void Draw(Warrior warrior)
        {
            if (_renderedWarriors.ContainsKey(warrior))
            {
                Application.Current?.Dispatcher?.Invoke(() =>
                {
                    _renderedWarriors[warrior].SetValue(Canvas.LeftProperty, (double)warrior.Position.X);
                    _renderedWarriors[warrior].SetValue(Canvas.TopProperty, (double)warrior.Position.Y);
                });
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Ellipse circle = new Ellipse()
                    {
                        Width = _radius,
                        Height = _radius,
                        StrokeThickness = 3
                    };
                    circle.Fill = _typeColors[warrior.Type];
                    circle.Stroke = _teamColors[warrior.TeamNumber];

                    circle.SetValue(Canvas.LeftProperty, (double)warrior.Position.X);
                    circle.SetValue(Canvas.TopProperty, (double)warrior.Position.Y);

                    _renderedWarriors.Add(warrior, circle);
                    _canvas.Children.Add(circle);

                    warrior.Dead += (() =>
                    {
                        Task.Run(() =>
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                circle.Fill = Brushes.GhostWhite;
                                circle.Stroke = Brushes.LightGray;
                                Thread.Sleep(800);
                                _canvas.Children.Remove(circle);
                            });
                        });
                    });
                });
            }
        }
    }
}
