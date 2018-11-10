using Unity.Entities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using Builders.Unit;
using Components.Common;
using Components.Units;
using Unity.Mathematics;
using UnityEngine.ResourceManagement;

public class SceneInitializer : MonoBehaviour
{
    // TODO: This is a quickly test
    public static SceneInitializer Instance{ get; private set; }
    
    public AssetReference UnitType01;
    public AssetReference UnitType02;
    public UnitConfiguration UnitType01Configuration;
    public UnitConfiguration UnitType02Configuration;
    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    
    private void Awake()
    {
        Instance = this;
    }
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitializeAfterSceneLoad()
    {
        // TODO: This is a quickly test
        var entityManager = World.Active.GetOrCreateManager<EntityManager>();
        var spawnInfo = entityManager.CreateEntity();
        entityManager.AddComponentData(spawnInfo, new SpawnInfo
        {
            Faction = Instance.SpawnPoint1.GetComponent<FactionMonoBehaviour>().Value,
            Heading = Instance.SpawnPoint1.GetComponent<Heading2DMonoBehaviour>().Value,
            Position = new float2(Instance.SpawnPoint1.transform.position.x, Instance.SpawnPoint1.transform.position.y),
            Path = Instance.SpawnPoint1.GetComponent<SpawnPointMonoBehaviour>().Path,
            Unit = UnitType.Type01
        });
        
        spawnInfo = entityManager.CreateEntity();
        entityManager.AddComponentData(spawnInfo, new SpawnInfo
        {
            Faction = Instance.SpawnPoint2.GetComponent<FactionMonoBehaviour>().Value,
            Heading = Instance.SpawnPoint2.GetComponent<Heading2DMonoBehaviour>().Value,
            Position = new float2(Instance.SpawnPoint2.transform.position.x, Instance.SpawnPoint2.transform.position.y),
            Path = Instance.SpawnPoint2.GetComponent<SpawnPointMonoBehaviour>().Path,
            Unit = UnitType.Type02
        });
    }
}