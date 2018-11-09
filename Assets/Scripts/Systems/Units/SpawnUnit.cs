using Builders.Unit;
using Components.Common;
using Components.Units;
using JetBrains.Annotations;
using Unity.Entities;
using UnityEngine;
using UnityEngine.ResourceManagement;

namespace Systems.Units
{
    [UsedImplicitly]
    public class SpawnUnit : ComponentSystem
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
            var entity = unit.GetComponent<GameObjectEntity>().Entity;

            var unitBuilder = new UnitBuilder(SceneInitializer.Instance.UnitConfiguration, spawnInfo);
            unitBuilder.Build(entity);
        }
    }
}