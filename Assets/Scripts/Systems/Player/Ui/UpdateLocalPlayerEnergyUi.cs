using Components.Player;
using Components.Player.Ui;
using JetBrains.Annotations;
using Unity.Entities;

namespace Systems.Player.Ui
{
    [UsedImplicitly]
    public class UpdateLocalPlayerEnergyUi : ComponentSystem
    {
#pragma warning disable 649
        private struct PlayersData
        {
            public ComponentArray<LocalPlayer> Players;
            public ComponentArray<PlayerUiController> PlayerUiControllers;
            public ComponentDataArray<Energy> Energies;
        }

        [Inject] private PlayersData _playerDataGroup;   
#pragma warning restore 649
        
        protected override void OnUpdate()
        {
            for (var i = 0; i < _playerDataGroup.Players.Length; i++)
            {
                var playerUiController = _playerDataGroup.PlayerUiControllers[i];
                var energy = _playerDataGroup.Energies[i];
                playerUiController.PlayerEnergyUi.UpdateEnergy(energy);
                playerUiController.SpawnButton.EnergyUpdated(energy);
            }
        }
    }
}