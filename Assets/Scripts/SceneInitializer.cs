using Unity.Entities;
using UnityEngine;
using Builders.Unit;
using Components.Common;
using Components.Units;
using Unity.Mathematics;
using UnityEngine.Assertions;

public class SceneInitializer : MonoBehaviour
{
    // TODO: This is a quickly test
    public static SceneInitializer Instance{ get; private set; }
    
    public IUnitFactory UnitFactory => _unitFactory;

    [SerializeField]
    private GameObject _spawnPoint1;
    [SerializeField]
    private GameObject _spawnPoint2;
    [SerializeField]
    private UnitFactory _unitFactory;

    
    private void Awake()
    {
        Assert.IsNull(Instance);
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
            Faction = Instance._spawnPoint1.GetComponent<FactionMonoBehaviour>().Value,
            Heading = Instance._spawnPoint1.GetComponent<Heading2DMonoBehaviour>().Value,
            Position = new float2(Instance._spawnPoint1.transform.position.x, Instance._spawnPoint1.transform.position.y),
            Path = Instance._spawnPoint1.GetComponent<SpawnPointMonoBehaviour>().Path,
            Unit = UnitType.Type01
        });
        
        spawnInfo = entityManager.CreateEntity();
        entityManager.AddComponentData(spawnInfo, new SpawnInfo
        {
            Faction = Instance._spawnPoint2.GetComponent<FactionMonoBehaviour>().Value,
            Heading = Instance._spawnPoint2.GetComponent<Heading2DMonoBehaviour>().Value,
            Position = new float2(Instance._spawnPoint2.transform.position.x, Instance._spawnPoint2.transform.position.y),
            Path = Instance._spawnPoint2.GetComponent<SpawnPointMonoBehaviour>().Path,
            Unit = UnitType.Type02
        });
    }
}