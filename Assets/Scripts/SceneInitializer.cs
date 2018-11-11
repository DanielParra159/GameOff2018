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

    }
}