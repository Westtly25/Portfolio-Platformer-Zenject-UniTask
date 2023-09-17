using UnityEngine;
using Scripts.Model;
using UnityEngine.UI;
using Scripts.UI.Widgets;
using System.Globalization;
using Scripts.Model.Definitions;
using Scripts.Model.Definitions.Player;
using Scripts.Model.Definitions.Localization;

namespace Scripts.UI.Windows.PlayerStats
{
    public class StatWidget : MonoBehaviour, IItemRenderer<StatDef>
    {
        [SerializeField] 
        private Image icon;
        [SerializeField]
        private Text name;
        [SerializeField]
        private Text currentValue;
        [SerializeField]
        private Text increaseValue;
        [SerializeField]
        private ProgressBarWidget _progress;
        [SerializeField]
        private GameObject _selector;

        private GameSession session;
        private StatDef data;

        private void Start()
        {
            session = FindObjectOfType<GameSession>();
            UpdateView();
        }

        public void SetData(StatDef data, int index)
        {
            this.data = data;
            if (session != null)
                UpdateView();
        }

        private void UpdateView()
        {
            var statsModel = session.StatsModel;

            icon.sprite = data.Icon;
            name.text = LocalizationManager.I.Localize(data.Name);
            var currentLevelValue = statsModel.GetValue(data.ID);
            currentValue.text = currentLevelValue.ToString(CultureInfo.InvariantCulture);

            var currentLevel = statsModel.GetCurrentLevel(data.ID);
            var nextLevel = currentLevel + 1;
            var nextLevelValue = statsModel.GetValue(data.ID, nextLevel);
            var increaseValue = nextLevelValue - currentLevelValue;
            this.increaseValue.text = $"+ {increaseValue}";
            this.increaseValue.gameObject.SetActive(increaseValue > 0);

            var maxLevel = CoreGameConfigs.ConfigsInstance.Player.GetStat(data.ID).Levels.Length - 1;
            _progress.SetProgress(currentLevel / (float) maxLevel);

            _selector.SetActive(statsModel.InterfaceSelectedStat.Value == data.ID);
        }

        public void OnSelect() =>
            session.StatsModel.InterfaceSelectedStat.Value = data.ID;
    }
}