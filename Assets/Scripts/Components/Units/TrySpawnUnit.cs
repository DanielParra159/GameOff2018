using Unity.Entities;

namespace Components.Units
{
    public struct TrySpawnUnit : IComponentData
    {
        public int Faction;
        public int Unit;
        public int Path;
        public int Energy;
    }
}