using Components.Common;
using Components.Units;
using JetBrains.Annotations;
using Unity.Entities;
using UnityEngine;
using UnityEngine.ResourceManagement;

namespace Systems.Units
{
    [UsedImplicitly]
    public class SpawnUnitSystem : ComponentSystem
    {
        private ComponentGroup _spawnInfoGroup;

        protected override void OnCreateManager()
        {
            _spawnInfoGroup = GetComponentGroup(ComponentType.ReadOnly(typeof(SpawnInfo)));
        }

        protected override void OnUpdate()
        {
            var spawnInfo = _spawnInfoGroup.GetComponentDataArray<SpawnInfo>();
            var entities = _spawnInfoGroup.GetEntityArray();

            for (int i = 0; i < entities.Length; ++i)
            {
                var info = spawnInfo[i];
                SceneInitializer.Instance.Unit.Instantiate<GameObject>().Completed +=
                    delegate(IAsyncOperation<GameObject> operation)
                    {
                        OnUnitInstantiate(operation.Result, info);
                    }; ;
                PostUpdateCommands.DestroyEntity(entities[i]);
            }
        }

        private void OnUnitInstantiate(GameObject unit, SpawnInfo spawnInfo)
        {
            unit.GetComponent<Position2D>().Value = spawnInfo.Position;
            unit.GetComponent<Faction>().Value = spawnInfo.Faction;
            unit.GetComponent<Heading2D>().Value = spawnInfo.Heading;
        }
    }
}