using Ui;
using UnityEngine;

namespace Components.Player.Ui
{
    public class SpawnBarUi : MonoBehaviour, ISpawnBarUi
    {
        [SerializeField] private SpawnButton _spawnButton;
        
        public void EnergyUpdated(Energy energy)
        {
            _spawnButton.EnergyUpdated(energy);
        }
    }
}