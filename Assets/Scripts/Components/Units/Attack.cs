using System;
using Unity.Entities;

namespace Components.Units
{
    [Serializable]
    public struct Attack : IComponentData
    {
        public float Damage;
        public float NextAvailableAttack;
        public float Rate;
    }
}