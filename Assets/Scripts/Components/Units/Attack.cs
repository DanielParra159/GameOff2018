using System;
using Unity.Entities;
using Utils;

namespace Components.Units
{
    [Serializable]
    public struct Attack : IComponentData
    {
        public float Damage;
        public Bool IsReady;
    }
}