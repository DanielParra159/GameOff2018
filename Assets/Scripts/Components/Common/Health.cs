using System;
using Unity.Entities;

namespace Components.Common
{
    [Serializable]
    public struct Health : IComponentData
    {
        public float Value;
    }
}