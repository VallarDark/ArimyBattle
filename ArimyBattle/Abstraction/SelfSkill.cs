namespace ArmyBattle.Abstraction
{
    using System;
    public abstract class SelfSkill : ISkill
    {
        protected int Strange;
        protected ISkill InnerSkill;
        protected int RollbackTime;
        protected int CastTime;

        protected int RollbackCounter;
        protected int CastCounter;

        protected SelfSkill(ISkill innerSkill = null)
        {
            InnerSkill = innerSkill;
        }

        protected virtual void Log(Warrior caster, Warrior target = null)
        {
            Console.WriteLine($"_____DefAura_____");
            Console.WriteLine($"Caster:\n{caster}");
            if (target == null)
            {
                return;
            }
            Console.WriteLine($"Target:\n{target}");
        }

        protected abstract void SkillLogic(Warrior caster);

        public void UseSkill(Warrior caster)
        {

            if (RollbackCounter <= 0)
            {
                if (CastCounter <= 0)
                {
                    SkillLogic(caster);
                    CastCounter = CastTime;
                }
                else
                {
                    CastCounter--;
                }

                RollbackCounter = RollbackTime;
            }
            else
            {
                RollbackCounter--;
            }

            InnerSkill?.UseSkill(caster);

        }
    }
}
