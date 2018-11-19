using Components.Common;
using Components.Path;
using Components.Player;
using Components.Units;
using JetBrains.Annotations;
using Unity.Entities;
using UnityEngine.Assertions;

namespace Systems.Units
{
    [UsedImplicitly]
    public class CheckForTrySpawnUnit : ComponentSystem
    {
#pragma warning disable 649
        private struct TrySpawnUnitData
        {
            public EntityArray Entities;
            public ComponentDataArray<TrySpawnUnit> TriesSpawnUnit;
        }

        private struct PathsManagerData
        {
            public ComponentArray<PathsManager> PathsManagers;
        }

        private struct PlayersData
        {
            public ComponentDataArray<Faction> Factions;
            public ComponentDataArray<Energy> Energies;
        }

        [Inject] private TrySpawnUnitData _trySpawnUnitData;
        [Inject] private PathsManagerData _pathsManagerData;
        [Inject] private PlayersData _playerDataGroup;
#pragma warning restore 649

        protected override void OnUpdate()
        {
            // TODO: is the path manager necessary?
            Assert.AreEqual(1, _pathsManagerData.PathsManagers.Length, "Only can exist one path manager");
            for (var i = 0; i < _trySpawnUnitData.Entities.Length; i++)
            {
                var entity = _trySpawnUnitData.Entities[i];
                var trySpawnUnit = _trySpawnUnitData.TriesSpawnUnit[i];
                for (var playerIndex = 0; playerIndex < _playerDataGroup.Factions.Length; ++playerIndex)
                {
                    if (_playerDataGroup.Factions[playerIndex].Value != trySpawnUnit.Faction)
                        continue;

                    var energy = _playerDataGroup.Energies[playerIndex];
                    if (energy.CurrentValue > trySpawnUnit.Energy)
                    {
                        _pathsManagerData.PathsManagers[0].SpawnUnit(trySpawnUnit);
                    }

                    break;
                }
                PostUpdateCommands.DestroyEntity(entity);
            }
        }
    }
}