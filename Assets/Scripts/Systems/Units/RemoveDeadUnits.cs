using Components.Common;
using Components.Units;
using JetBrains.Annotations;
using Unity.Entities;
using UnityEngine.AddressableAssets;

namespace Systems.Units
{
    [UsedImplicitly]
    [UpdateAfter(typeof(CheckForDeathUnits))]
    public class DestroyUnits : ComponentSystem
    {
#pragma warning disable 649
        private struct Entities
        {
            public readonly int Length;
            public GameObjectArray GameObjects;
            public ComponentDataArray<Unit> Units;
            public ComponentDataArray<Dead> Healths;
        }
        [Inject] private Entities _entities;
#pragma warning restore 649
        
        protected override void OnUpdate()
        {
            for (var i = 0; i < _entities.Length; ++i)
            {
                Addressables.ReleaseInstance(_entities.GameObjects[i]);
            }
        }
    }
}