namespace ArmyBattle.Abstraction
{
    using System.Linq;
    public abstract class AoeDebuffSkill : AoeSkill
    {
        protected override Warrior[] GetTargets(Warrior caster, params Warrior[] targets)
        {
            return base.GetTargets(caster, targets).Where(t => t.CommandNumber != caster.CommandNumber).ToArray();
        }
    }
}
