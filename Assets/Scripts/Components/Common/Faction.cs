using System;
using Unity.Entities;

namespace Components.Common
{
    [Serializable]
    public struct Faction : IComponentData
    {
        public int Value;
    }
}