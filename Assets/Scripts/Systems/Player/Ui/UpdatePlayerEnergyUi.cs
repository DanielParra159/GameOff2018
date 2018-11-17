using Components.Player;
using Components.Player.Ui;
using JetBrains.Annotations;
using Unity.Entities;

namespace Systems.Player.Ui
{
    [UsedImplicitly]
    public class UpdatePlayerEnergyUi : ComponentSystem
    {
        public struct PlayersData
        {
            public ComponentArray<Components.Player.Player> Players;
            public ComponentArray<PlayerUiController> PlayerUiControllers;
            public ComponentDataArray<Energy> Energies;
        }

        [Inject] private PlayersData _playerDataGroup;   
        
        protected override void OnUpdate()
        {
            for (var i = 0; i < _playerDataGroup.Players.Length; i++)
            {
                var playerUiController = _playerDataGroup.PlayerUiControllers[i];
                var energy = _playerDataGroup.Energies[i];
                playerUiController.PlayerEnergyUi.UpdateEnergy(energy);
            }
        }
    }
}