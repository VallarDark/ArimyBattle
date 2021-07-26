using System;

namespace BattleView.Renderers
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using ArmyBattle.Abstraction;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    /// <summary>
    ///  draws the position of the warriors, their HP and ID.
    /// allows you to clear the field from the graphic display of a warrior or all warriors.
    /// </summary>
    public class Renderer
    {
        private readonly Dictionary<Warrior, List<FrameworkElement>> _renderedWarriors;
        private readonly List<Warrior> _warriors;
        private readonly Canvas _canvas;

        private readonly int _radius;
        private readonly Dictionary<int, SolidColorBrush> _teamColors;
        private readonly Dictionary<string, SolidColorBrush> _typeColors;
        public Renderer(List<Warrior> warriors, Canvas canvas, int radius = 15)
        {
            _renderedWarriors = new Dictionary<Warrior, List<FrameworkElement>>();
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
            Parallel.For(0, _warriors.Count, index =>
            {
                if (index < _warriors.Count && index >= 0)
                {
                    try
                    {
                        var warrior = _warriors[index];
                        if (warrior != null)
                        {
                            Draw(warrior);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
            });
        }

        public void Clear()
        {
            _canvas.Children.Clear();
            _renderedWarriors.Clear();
        }

        public void Remove(int id)
        {
            var warrior = _warriors.FirstOrDefault(w => w.Id == id);
            if (warrior != null)
            {
                var children = _renderedWarriors[warrior];
                foreach (var child in children)
                {
                    _canvas.Children.Remove(child);
                }

                _renderedWarriors.Remove(warrior);
            }
        }


        private void EditView(Warrior warrior)
        {
            var warriorView = _renderedWarriors[warrior][0];
            var warriorId = _renderedWarriors[warrior][1] as TextBlock;
            var warriorHp = _renderedWarriors[warrior][2] as TextBlock;

            warriorView?.SetValue(Canvas.LeftProperty, (double)warrior.Position.X);
            warriorView?.SetValue(Canvas.TopProperty, (double)warrior.Position.Y);

            warriorId?.SetValue(Canvas.LeftProperty, (double)warrior.Position.X - 25);
            warriorId?.SetValue(Canvas.TopProperty, (double)warrior.Position.Y - 15);

            if (warriorId != null)
            {
                warriorId.Text = $"id: {warrior.Id}";
            }

            warriorHp?.SetValue(Canvas.LeftProperty, (double)warrior.Position.X + 15);
            warriorHp?.SetValue(Canvas.TopProperty, (double)warrior.Position.Y - 15);

            if (warriorHp != null)
            {
                warriorHp.Text = $"hp: {warrior.Hp}";
            }
        }

        private void AddView(Warrior warrior)
        {
            Ellipse circle = new Ellipse()
            {
                Width = _radius,
                Height = _radius,
                StrokeThickness = 3,
                Fill = _typeColors[warrior.Type],
                Stroke = _teamColors[warrior.TeamNumber]
            };
            circle.SetValue(Canvas.LeftProperty, (double)warrior.Position.X);
            circle.SetValue(Canvas.TopProperty, (double)warrior.Position.Y);


            TextBlock idBlock = new TextBlock { Text = $"id: {warrior.Id}" };
            idBlock.SetValue(Canvas.LeftProperty, (double)warrior.Position.X - 25);
            idBlock.SetValue(Canvas.TopProperty, (double)warrior.Position.Y - 15);


            TextBlock hpBlock = new TextBlock { Text = $"hp: {warrior.Hp}" };
            hpBlock.SetValue(Canvas.LeftProperty, (double)warrior.Position.X + 15);
            hpBlock.SetValue(Canvas.TopProperty, (double)warrior.Position.Y - 15);

            _renderedWarriors.Add(warrior, new List<FrameworkElement>() { circle, idBlock, hpBlock });

            _canvas.Children.Add(circle);
            _canvas.Children.Add(idBlock);
            _canvas.Children.Add(hpBlock);

            warrior.Dead += (() =>
            {
                Task.Run(() =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Task.Run(() =>
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                circle.Fill = Brushes.Black;
                                circle.Stroke = Brushes.Black;
                            });
                        });

                        Thread.Sleep(800);
                        _canvas.Children.Remove(circle);
                        _canvas.Children.Remove(idBlock);
                        _canvas.Children.Remove(hpBlock);
                    });
                });
            });
        }

        private void Draw(Warrior warrior)
        {
            if (_renderedWarriors.ContainsKey(warrior))
            {
                Application.Current?.Dispatcher?.Invoke(() =>
                {
                    EditView(warrior);
                });
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    AddView(warrior);
                });
            }
        }
    }
}
