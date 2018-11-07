using Unity.Entities;
using Unity.Mathematics;

namespace Components.Units
{
    public struct SpawnInfo : IComponentData
    {
        public float2 Position;
        public float2 Heading;
        public int Faction;
    }
}