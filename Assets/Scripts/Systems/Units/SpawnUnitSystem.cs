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
            var spawnsInfo = _spawnInfoGroup.GetComponentDataArray<SpawnInfo>();
            var entities = _spawnInfoGroup.GetEntityArray();

            for (int i = 0; i < entities.Length; ++i)
            {
                var spawnInfo = spawnsInfo[i];
                SceneInitializer.Instance.Unit.Instantiate<GameObject>().Completed +=
                    delegate(IAsyncOperation<GameObject> operation)
                    {
                        SetUnitConfiguration(operation.Result, spawnInfo);
                    };
                PostUpdateCommands.DestroyEntity(entities[i]);
            }
        }

        private void SetUnitConfiguration(GameObject unit, SpawnInfo spawnInfo)
        {
            unit.GetComponent<Position2D>().Value = spawnInfo.Position;
            unit.GetComponent<Faction>().Value = spawnInfo.Faction;
            unit.GetComponent<Heading2D>().Value = spawnInfo.Heading;
            unit.GetComponent<Unit>().Path = spawnInfo.Path;
        }
    }
}