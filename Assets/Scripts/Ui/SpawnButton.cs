using Components.Path;
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
            PathsManager.Instance.SpawnUnit(faction, path, unitType);
        }
    }
}