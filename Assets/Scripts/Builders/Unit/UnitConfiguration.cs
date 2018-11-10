using Components.Common;
using Components.Units;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Builders.Unit
{
    [CreateAssetMenu(fileName = "UnitConfiguration", menuName = Constants.AssetsMenu + "Units/UnitConfiguration", order = 1)]
    public class UnitConfiguration : ScriptableObject
    {
        [SerializeField]
        private AssetReference _assetReference;
        [SerializeField]
        private Health _health;
        [SerializeField]
        private Attack _attack;
        [SerializeField]
        private Range _range;
        [SerializeField]
        private MoveSpeed _moveSpeed;

        public AssetReference AssetReference => _assetReference;
        public Health Health => _health;
        public Attack Attack => _attack;
        public Range Range => _range;
        public MoveSpeed MoveSpeed => _moveSpeed;
    }
}