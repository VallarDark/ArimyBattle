using System;
using System.Collections.Generic;
using System.Text;
using ArmyBattle.Abstraction;

namespace ArmyBattle.WarriorsDraw
{
    class SwordsmanDraw:DrawWarrior
    {
        public SwordsmanDraw(int maxHp) : base(maxHp)
        {
            CharacterChar = 'S';
        }
    }
}
