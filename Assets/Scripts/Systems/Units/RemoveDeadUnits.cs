using System.Collections.Generic;
using Components.Common;
using JetBrains.Annotations;
using Unity.Entities;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Systems.Units
{
    [UsedImplicitly]
    public class DestroyUnits : ComponentSystem
    {
#pragma warning disable 649
        private struct Entities
        {
            public readonly int Length;
            public GameObjectArray GameObjects;
            public ComponentDataArray<Health> Healths;
        }
        [Inject] private Entities _entities;
#pragma warning restore 649
        
        protected override void OnUpdate()
        {
            var unitsToDestroy = new List<GameObject>();
            for (var i = 0; i < _entities.Length; ++i)
            {

                if (_entities.Healths[i].Value <= 0)
                {
                    unitsToDestroy.Add(_entities.GameObjects[i]);
                }
            }

            foreach (var go in unitsToDestroy)
            {
                Addressables.ReleaseInstance(go);
            }
        }
    }
}