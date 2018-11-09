using Unity.Entities;
using Unity.Mathematics;

namespace Components.Common
{
    public struct Position2D : IComponentData
    {
        public float2 Value;
    }
}