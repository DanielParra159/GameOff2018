using System;
using UnityEngine;

namespace Components.Units
{
    [Serializable]
    public class Attack : MonoBehaviour
    {
        public float Damage;
        public float NextAvailableAttack;
        public float Rate;
    }
}