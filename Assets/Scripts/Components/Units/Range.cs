using System;
using Unity.Entities;

namespace Components.Units
{
    [Serializable]
    public struct Range : IComponentData
    {
        public float Value;
    }
}