
namespace ArmyBattle.Abstraction
{
    using System;

    public abstract class DrawWarrior
    {
        protected int DrawHpSegmentsCount { get; set; } = 5;
        protected char DrawHpSegmentsChar { get; set; } = '.';
        protected int MaxHp { get; set; }
        protected char CharacterChar { get; set; } = 'C';

        protected DrawWarrior(int maxHp)
        {
            MaxHp = maxHp;
        }
        protected virtual void DrawHp(int hp)
        {
            var hpSegment = MaxHp / DrawHpSegmentsCount + MaxHp % DrawHpSegmentsCount;
            var currentHp = hp;
            for (int i = 0; i < DrawHpSegmentsCount; i++)
            {
                Console.ForegroundColor = currentHp > 0 ? ConsoleColor.Green : ConsoleColor.Gray;
                Console.Write($"{DrawHpSegmentsChar}");
                currentHp -= hpSegment;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
        public virtual void Draw(int hp)
        {
            DrawHp(hp);
            Console.WriteLine($" ({CharacterChar})");
        }
        public virtual void Die()
        {

            Console.WriteLine(@" \X/");
        }
    }
}
