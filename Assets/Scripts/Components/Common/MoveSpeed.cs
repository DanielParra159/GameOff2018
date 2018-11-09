using System;
using Unity.Entities;

namespace Components.Common
{
    [Serializable]
    public struct MoveSpeed : IComponentData
    {
        public float Value;
    }
}