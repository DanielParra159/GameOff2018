using Components.Units;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions;

namespace Components.Path
{
    public class PathsManager : MonoBehaviour
    {
        // TODO: This is a quickly test
        public static PathsManager Instance{ get; private set; }
        
        [SerializeField]
        private Path[] _paths;

        private void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;
        }

        public void SpawnUnit(int faction, int path, int unitType)
        {
            var spawnPoint = _paths[path]._spawnPoint[faction];
            var position = spawnPoint.transform.position;
            var heading = spawnPoint.Heading;
            
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();
            var spawnInfo = entityManager.CreateEntity();
            entityManager.AddComponentData(spawnInfo, new SpawnInfo
            {
                Faction = faction,
                Heading = heading,
                Position = new float2(position.x, position.y),
                Path = path,
                Unit = unitType
            });
        }
    }
}