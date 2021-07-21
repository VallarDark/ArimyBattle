﻿namespace ArmyBattle.Abstraction
{
    public interface ISkill
    {
        public int Strange { get; set; }
        public ISkill InnerSkill { get; set; }
        public void UseSkill(Warrior caster);
    }
}
