using Components.Common;
using Components.Units;
using UnityEngine;

namespace Builders.Unit
{
    [CreateAssetMenu(fileName = "Data", menuName = "Unit/UnitConfiguration", order = 1)]
    public class UnitConfiguration : ScriptableObject
    {
        [SerializeField]
        private Health _health;
        [SerializeField]
        private Attack _attack;
        [SerializeField]
        private Range _range;
        [SerializeField]
        private MoveSpeed _moveSpeed;

        public Health Health => _health;
        public Attack Attack => _attack;
        public Range Range => _range;
        public MoveSpeed MoveSpeed => _moveSpeed;
    }
}