using Components.Units;
using Unity.Entities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.ResourceManagement;

namespace Builders.Unit
{
    public class UnitFactory
    {
        private readonly UnitBuilder _unitBuilder;

        public UnitFactory()
        {
            _unitBuilder = new UnitBuilder();
        }
    
        public void Instance(SpawnInfo spawnInfo)
        {
            // TODO: find a better way to do this differentiation
            AssetReference unitReference = null;
            UnitConfiguration unitConfiguration = null;
            switch (spawnInfo.Unit)
            {
                case UnitType.Type01:
                    unitReference = SceneInitializer.Instance.UnitType01;
                    unitConfiguration = SceneInitializer.Instance.UnitType01Configuration;
                    break;
                case UnitType.Type02:
                    unitReference = SceneInitializer.Instance.UnitType02;
                    unitConfiguration = SceneInitializer.Instance.UnitType02Configuration;
                    break;
                default:
                    Assert.IsTrue(false, spawnInfo.Unit + " is an invalid type");
                    break;
            }

            unitReference.Instantiate<GameObject>().Completed +=
                delegate(IAsyncOperation<GameObject> operation)
                {
                    SetUnitConfiguration(operation.Result, spawnInfo, unitConfiguration);
                };
        }

        private void SetUnitConfiguration(GameObject unit, SpawnInfo spawnInfo, UnitConfiguration unitConfiguration)
        {
            var entity = unit.GetComponent<GameObjectEntity>().Entity;

            _unitBuilder.Build(unitConfiguration, spawnInfo, entity);
        }
    }
}