using Components.Units;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.ResourceManagement;

namespace Builders.Units
{
    [CreateAssetMenu(fileName = "UnitFactory", menuName = Constants.AssetsMenu + "Units/UnitFactory", order = 1)]
    public class UnitFactory : ScriptableObject, IUnitFactory
    {
        [SerializeField] private UnitConfiguration[] _archetypes;

        private readonly UnitBuilder _unitBuilder;

        private UnitFactory()
        {
            _unitBuilder = new UnitBuilder();
        }

        public void Instance(SpawnInfo spawnInfo)
        {
            Assert.IsTrue(spawnInfo.Unit <= _archetypes.Length, spawnInfo.Unit + " is an invalid type");

            // TODO: find a better way to do this differentiation
            var unitReferences = _archetypes[spawnInfo.Unit];

            unitReferences.AssetReference
                    .Instantiate<GameObject>(new Vector3(spawnInfo.Position.x, spawnInfo.Position.y),
                        Quaternion.identity).Completed +=
                delegate(IAsyncOperation<GameObject> operation)
                {
                    SetUnitConfiguration(operation.Result, spawnInfo, unitReferences);
                };
        }

        private void SetUnitConfiguration(GameObject unit, SpawnInfo spawnInfo, UnitConfiguration unitConfiguration)
        {
            var entity = unit.GetComponent<GameObjectEntity>().Entity;

            _unitBuilder.Build(unitConfiguration, spawnInfo, entity);
        }
    }
}