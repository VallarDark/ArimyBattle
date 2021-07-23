namespace ArmyBattle.Abstraction
{
    using System.Threading.Tasks;
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

        protected abstract void SkillLogic(Warrior caster);

        public async void UseSkill(Warrior caster)
        {
            await Task.Run((() =>
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
            }));
        }
    }
}
