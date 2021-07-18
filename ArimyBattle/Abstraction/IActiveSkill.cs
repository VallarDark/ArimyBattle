namespace ArmyBattle.Abstraction
{
    public interface IActiveSkill
    {
        public int RollbackTime { get; set; }
        public int CastTime { get; set; }
    }
}
