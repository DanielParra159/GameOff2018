using Builders.Unit;
using Components.Units;
using JetBrains.Annotations;
using Unity.Entities;

namespace Systems.Units
{
    [UsedImplicitly]
    public class SpawnUnit : ComponentSystem
    {
        private ComponentGroup _spawnInfoGroup;
        private UnitFactory _unitFactory;

        protected override void OnCreateManager()
        {
            _spawnInfoGroup = GetComponentGroup(ComponentType.ReadOnly(typeof(SpawnInfo)));
            _unitFactory = new UnitFactory();
        }

        protected override void OnUpdate()
        {
            var spawnsInfo = _spawnInfoGroup.GetComponentDataArray<SpawnInfo>();
            var entities = _spawnInfoGroup.GetEntityArray();

            for (var i = 0; i < entities.Length; ++i)
            {
                _unitFactory.Instance(spawnsInfo[i]);
                PostUpdateCommands.DestroyEntity(entities[i]);
            }
        }
    }
}