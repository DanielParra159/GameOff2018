using Components.Units;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions;

namespace Components.Path
{
    public class PathsManager : MonoBehaviour
    {
        [SerializeField]
        private Path[] _paths;

        public void SpawnUnit(TrySpawnUnit trySpawnUnit)
        {
            var spawnPoint = _paths[trySpawnUnit.Path]._spawnPoint[trySpawnUnit.Faction];
            var position = spawnPoint.transform.position;
            var heading = spawnPoint.Heading;
            
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();
            var spawnInfo = entityManager.CreateEntity();
            entityManager.AddComponentData(spawnInfo, new SpawnInfo
            {
                Faction = trySpawnUnit.Faction,
                Heading = heading,
                Position = new float2(position.x, position.y),
                Path = trySpawnUnit.Path,
                Unit = trySpawnUnit.Unit,
                Energy = trySpawnUnit.Energy
            });
        }
    }
}