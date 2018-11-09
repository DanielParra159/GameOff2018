using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Components.Common
{
    [Serializable]
    public struct Heading2D : IComponentData
    {
        public float2 Value;
    }
}