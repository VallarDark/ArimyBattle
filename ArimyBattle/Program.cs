using System;
using System.Numerics;
using ArmyBattle.Warriors;
using ArmyBattle.WarriorsDraw;

namespace ArmyBattle
{
    class Program
    {
        static void Main()
        {
            var sm = new Swordsman(new Vector2(0, 0), 1);
            sm.Draw();

            sm.Hp -= 116;
            sm.Draw();
        }
    }
}
