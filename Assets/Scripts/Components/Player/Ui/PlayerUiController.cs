using Unity.Entities;
using UnityEngine;

namespace Components.Player.Ui
{
    [RequireComponent(typeof(GameObjectEntity))]
    public class PlayerUiController : MonoBehaviour
    {
        [SerializeField] private PlayerEnergyUi _playerEnergyUi;
        [SerializeField] private GameObjectEntity _gameObjectEntity;

        public IPlayerEnergyUi PlayerEnergyUi => _playerEnergyUi;

        private void Start()
        {
            // TODO: Builder
            var entityManager = World.Active.GetExistingManager<EntityManager>();
            var entity = _gameObjectEntity.Entity;
            entityManager.AddComponentData(entity, new Energy{CurrentValue = 0, MaxValue = 10});
            entityManager.AddComponentData(entity, new EnergyRecharger{EnergyPerSecond = 1});
        }
    }
}