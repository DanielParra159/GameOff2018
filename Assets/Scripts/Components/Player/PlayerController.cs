using Components.Common;
using Components.Player.Ui;
using Unity.Entities;
using UnityEngine;

namespace Components.Player
{
    [RequireComponent(typeof(GameObjectEntity))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private bool _isLocalPlayer;
        [SerializeField] private int _faction;
        [SerializeField] private PlayerEnergyUi _playerEnergyUi;
        [SerializeField] private SpawnBarUi _spawnBarUi;
        [SerializeField] private GameObjectEntity _gameObjectEntity;

        public IPlayerEnergyUi PlayerEnergyUi => _playerEnergyUi;
        public ISpawnBarUi SpawnBarUi => _spawnBarUi;

        private void Start()
        {
            // TODO: Builder
            var entityManager = World.Active.GetExistingManager<EntityManager>();
            var entity = _gameObjectEntity.Entity;
            if (_isLocalPlayer)
            {
                entityManager.AddComponentData(entity, new LocalPlayer());
            }

            entityManager.AddComponentData(entity, new Energy{CurrentValue = 0, MaxValue = 10});
            entityManager.AddComponentData(entity, new EnergyRecharger{EnergyPerSecond = 1});
            entityManager.AddComponentData(entity, new Faction{Value = _faction});
        }
    }

    
}