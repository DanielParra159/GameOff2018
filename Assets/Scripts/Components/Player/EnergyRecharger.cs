using Unity.Entities;

namespace Components.Player
{
    public struct EnergyRecharger : IComponentData
    {
        public float EnergyPerSecond;
    }
}