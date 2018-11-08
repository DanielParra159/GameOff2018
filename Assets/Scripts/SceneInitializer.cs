using Unity.Entities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using Components.Common;
using Components.Units;
using Unity.Mathematics;
using UnityEngine.ResourceManagement;

public class SceneInitializer : MonoBehaviour
{
    // TODO: This is a quickly test
    public static SceneInitializer Instance{ get; private set; }
    
    public AssetReference Unit;
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
            Faction = Instance.SpawnPoint1.GetComponent<Faction>().Value,
            Heading = Instance.SpawnPoint1.GetComponent<Heading2D>().Value,
            Position = new float2(Instance.SpawnPoint1.transform.position.x, Instance.SpawnPoint1.transform.position.y),
            Path = Instance.SpawnPoint1.GetComponent<SpawnPoint>().Path,
        });
        
        spawnInfo = entityManager.CreateEntity();
        entityManager.AddComponentData(spawnInfo, new SpawnInfo
        {
            Faction = Instance.SpawnPoint2.GetComponent<Faction>().Value,
            Heading = Instance.SpawnPoint2.GetComponent<Heading2D>().Value,
            Position = new float2(Instance.SpawnPoint2.transform.position.x, Instance.SpawnPoint2.transform.position.y)
        });



    }
}