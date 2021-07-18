using System.Collections.Generic;
using System.Numerics;

namespace ArmyBattle.Abstraction
{
    public abstract class AoeSkill:StrangerSkill
    {
        protected virtual Warrior[] GetTargets(Warrior caster, params Warrior[] targets)
        {
            var result = new List<Warrior>();
            foreach (var target in targets)
            {
                if (Vector2.Distance(caster.Position,target.Position)<=Radius)
                {
                    result.Add(target);
                }
            }

            return result.ToArray();
        }
    }
}
