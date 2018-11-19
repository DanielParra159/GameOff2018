using Unity.Entities;

namespace Components.Units
{
    public struct SpawnUnitEvent : IComponentData
    {
        public int Faction;
        public int Unit;
        public int Path;
    }
}