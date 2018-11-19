using Components.Common;
using Components.Player;
using Components.Units;
using JetBrains.Annotations;
using Unity.Entities;
using UnityEngine.Assertions;

namespace Systems.Units
{
    [UsedImplicitly]
    public class SpawnUnit : ComponentSystem
    {
        private struct PlayersData
        {
            public ComponentDataArray<Faction> Factions;
            public ComponentDataArray<Energy> Energies;
        }
        
        [Inject] private PlayersData _playerDataGroup;
        private ComponentGroup _spawnInfoGroup;

        protected override void OnCreateManager()
        {
            _spawnInfoGroup = GetComponentGroup(ComponentType.ReadOnly(typeof(SpawnInfo)));
        }

        protected override void OnUpdate()
        {
            var spawnsInfo = _spawnInfoGroup.GetComponentDataArray<SpawnInfo>();
            var entities = _spawnInfoGroup.GetEntityArray();

            for (var i = 0; i < entities.Length; ++i)
            {
                var spawnInfo = spawnsInfo[i];

                for (var playerIndex = 0; playerIndex < _playerDataGroup.Factions.Length; ++playerIndex)
                {
                    if (_playerDataGroup.Factions[playerIndex].Value != spawnInfo.Faction)
                        continue;

                    var energy = _playerDataGroup.Energies[playerIndex];
                    Assert.IsTrue(energy.CurrentValue >= spawnInfo.Energy, "Don't have enough energy");
                    energy.CurrentValue -= spawnInfo.Energy;
                    _playerDataGroup.Energies[playerIndex] = energy;
                }

                SceneInitializer.Instance.UnitFactory.Instance(spawnInfo);
                PostUpdateCommands.DestroyEntity(entities[i]);
            }
        }
    }
}