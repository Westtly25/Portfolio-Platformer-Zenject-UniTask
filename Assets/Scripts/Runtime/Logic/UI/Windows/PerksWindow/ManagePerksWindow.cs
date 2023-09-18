using System;
using Scripts.Model;
using Scripts.Model.Definitions;
using Scripts.Model.Definitions.Localization;
using Scripts.Model.Definitions.Repositories;
using Scripts.UI.Widgets;
using Scripts.Utilities.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Windows.PerksWindow
{
    public class ManagePerksWindow : AnimatedWindow
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _useButton;
        [SerializeField] private Text _infoText;
        [SerializeField] private ItemWidget _price;

        private PredefinedDataGroup<PerkConfig, PerkWidget> _dataGroup;
        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private GameSession _session;

        protected override void Start()
        {
            base.Start();

            _dataGroup = new PredefinedDataGroup<PerkConfig, PerkWidget>(_container);
            _session = FindObjectOfType<GameSession>();

            _trash.Retain(_session.PerksModel.Subscribe(OnPerksChanged));

            _trash.Retain(_buyButton.onClick.Subscribe(OnBuy));
            _trash.Retain(_useButton.onClick.Subscribe(OnUse));

            OnPerksChanged();
        }

        private void OnPerksChanged()
        {
            _dataGroup.SetData(CoreGameConfigs.ConfigsInstance.Perks.All);

            var selected = _session.PerksModel.InterfaceSelection.Value;

            _useButton.gameObject.SetActive(_session.PerksModel.IsUnlocked(selected));
            _useButton.interactable = _session.PerksModel.Used != selected;

            _buyButton.gameObject.SetActive(!_session.PerksModel.IsUnlocked(selected));
            _buyButton.interactable = _session.PerksModel.CanBuy(selected);

            var def = CoreGameConfigs.ConfigsInstance.Perks.Get(selected);
            _price.SetData(def.Price);

            _infoText.text = LocalizationManager.I.Localize(def.Info);
        }

        private void OnUse()
        {
            var selected = _session.PerksModel.InterfaceSelection.Value;
            _session.PerksModel.SelectPerk(selected);
        }

        private void OnBuy()
        {
            var selected = _session.PerksModel.InterfaceSelection.Value;
            _session.PerksModel.Unlock(selected);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}