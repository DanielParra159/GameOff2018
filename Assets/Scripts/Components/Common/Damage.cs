using Unity.Entities;

namespace Components.Common
{
    [InternalBufferCapacity(4)]
    public struct Damage : IBufferElementData
    {
        public float Value;
    }
}