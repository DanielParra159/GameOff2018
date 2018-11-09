using System;
using Unity.Entities;

namespace Components.Units
{
    [Serializable]
    public struct Unit : IComponentData
    {
        public int Path;
    }
}