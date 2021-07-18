namespace ArmyBattle.Abstraction
{
    using System.Linq;
    using System.Numerics;
    public abstract class BuffSkill : StrangerSkill
    {
        protected virtual Warrior GetTarget(Warrior caster, params Warrior[] targets)
        {
            return targets.FirstOrDefault(t => Vector2.Distance(caster.Position, t.Position) <= Radius && t.CommandNumber==caster.CommandNumber);
        }
    }
}
