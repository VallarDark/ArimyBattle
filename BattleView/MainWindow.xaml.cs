using System.Numerics;
using ArmyBattle.Factories;

namespace BattleView
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using ArmyBattle.Army;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Battle _battle;

        public MainWindow()
        {
            InitializeComponent();
            _battle = new Battle();
            BattleCanvas = new Canvas();
            Type.SelectionMode = SelectionMode.Single;
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selected = Type.SelectedItem.ToString();

            if (float.TryParse(PositionX.Text, out var positionX) && float.TryParse(PositionY.Text, out var positionY) &&
                int.TryParse(TeamNumber.Text, out var teamNumber))
            {
                if (selected == "Archer")
                {
                    _battle.AddWarrior<ArcherFactory>(new Vector2(positionX, positionY), teamNumber);
                }
                else if (selected == "LanceKnight")
                {
                    _battle.AddWarrior<LanceKnightFactory>(new Vector2(positionX, positionY), teamNumber);
                }
                else if (selected == "Swordsman")
                {
                    _battle.AddWarrior<SwordsmanFactory>(new Vector2(positionX, positionY), teamNumber);
                }
            }
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            _battle.Start();
        }

        private void StopButton_OnClick(object sender, RoutedEventArgs e)
        {
            _battle.Stop();
        }

        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            _battle.Clear();
        }

        private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(RemoveId.Text, out var removeId))
            {
                _battle.Remove(removeId);
            }
        }
    }
}
