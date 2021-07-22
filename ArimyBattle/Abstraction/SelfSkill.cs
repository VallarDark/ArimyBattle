namespace ArmyBattle.Abstraction
{
    public abstract class SelfSkill : ISkill
    {
        protected delegate void Skill(Warrior caster);
        protected Skill SkillFunk;

        protected Warrior Caster;
        protected int Strange;
        protected ISkill InnerSkill;
        protected int RollbackTime;
        protected int CastTime;

        protected int RollbackCounter;
        protected int CastCounter;

        protected SelfSkill(Warrior caster, ISkill innerSkill = null)
        {
            Caster = caster;
            InnerSkill = innerSkill;
        }

        public void UseSkill(Warrior caster)
        {
            if (RollbackCounter<=0)
            {
                if (CastCounter<=0)
                {
                    SkillFunk(caster);
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
