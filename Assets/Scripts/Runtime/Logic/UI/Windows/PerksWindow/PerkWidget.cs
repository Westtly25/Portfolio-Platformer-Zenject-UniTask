using Scripts.Model;
using Scripts.Model.Definitions.Repositories;
using Scripts.UI.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Windows.PerksWindow
{
    public class PerkWidget : MonoBehaviour, IItemRenderer<PerkConfig>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _isUsed;
        [SerializeField] private GameObject _isSelected;
        [SerializeField] private GameObject _isLocked;

        private GameSession _session;
        private PerkConfig _data;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            UpdateView();
        }

        public void SetData(PerkConfig data, int index)
        {
            _data = data;

            if (_session != null)
                UpdateView();
        }

        private void UpdateView()
        {
            _icon.sprite = _data.Icon;
            _isUsed.SetActive(_session.PerksModel.IsUsed(_data.Id));
            _isSelected.SetActive(_session.PerksModel.InterfaceSelection.Value == _data.Id);
            _isLocked.SetActive(!_session.PerksModel.IsUnlocked(_data.Id));
        }

        public void OnSelect()
        {
            _session.PerksModel.InterfaceSelection.Value = _data.Id;
        }
    }
}