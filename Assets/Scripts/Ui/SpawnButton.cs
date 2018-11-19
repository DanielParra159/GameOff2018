using Components.Path;
using Components.Player;
using Components.Units;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    [RequireComponent(typeof(Button))]
    public class SpawnButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        // TODO: Quickly test
        [SerializeField] private int _faction;
        [SerializeField] private int _path;
        [SerializeField] private int _unitType;
        [SerializeField] private int _energyCost;

        public void EnergyUpdated(Energy energy)
        {
            _button.interactable = energy.CurrentValue >= _energyCost;
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            SpawnUnit(_faction, _path, _unitType);
        }

        private void SpawnUnit(int faction, int path, int unitType)
        {
            var entityManager = World.Active.GetOrCreateManager<EntityManager>();
            var spawnInfo = entityManager.CreateEntity();
            entityManager.AddComponentData(spawnInfo, new TrySpawnUnit
            {
                Faction = faction,
                Path = path,
                Unit = unitType,
                Energy = _energyCost
            });
        }
    }

 
}