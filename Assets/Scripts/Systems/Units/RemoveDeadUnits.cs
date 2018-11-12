using Components.Common;
using Components.Units;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Entities;
using UnityEngine.AddressableAssets;

namespace Systems.Units
{
    [UsedImplicitly]
    public class DestroyUnits : ComponentSystem
    {
#pragma warning disable 649
        private struct Data
        {
            public readonly int Length;
            public GameObjectArray GameObjects;
            [ReadOnly] public ComponentDataArray<Unit> Units;
            [ReadOnly] public ComponentDataArray<Dead> Healths;
        }
        [Inject] private Data _data;
#pragma warning restore 649
        
        protected override void OnUpdate()
        {
            for (var i = 0; i < _data.Length; ++i)
            {
                Addressables.ReleaseInstance(_data.GameObjects[i]);
            }
        }
    }
}