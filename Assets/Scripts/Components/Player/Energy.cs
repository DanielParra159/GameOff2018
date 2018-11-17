using Unity.Entities;

namespace Components.Player
{
    public struct Energy : IComponentData
    {
        public float CurrentValue;
        public float MaxValue;
    }
}