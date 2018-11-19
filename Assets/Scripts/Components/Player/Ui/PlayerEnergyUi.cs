using UnityEngine;
using UnityEngine.UI;

namespace Components.Player.Ui
{
    public class PlayerEnergyUi : MonoBehaviour, IPlayerEnergyUi
    {
        [SerializeField] private Image _energyBar;
        public void UpdateEnergy(Energy energy)
        {
            _energyBar.fillAmount = energy.CurrentValue / energy.MaxValue;
            Debug.Log(energy.CurrentValue);
        }
    }
}