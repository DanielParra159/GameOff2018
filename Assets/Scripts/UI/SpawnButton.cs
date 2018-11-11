using Components.Path;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class SpawnButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        // TODO: Quickly test
        [SerializeField] private int _faction;
        [SerializeField] private int _path;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            SpawnUnit(_faction, _path, 0);
        }

        private void SpawnUnit(int faction, int path, int unitType)
        {
            PathsManager.Instance.SpawnUnit(faction, path, unitType);
        }
    }
}