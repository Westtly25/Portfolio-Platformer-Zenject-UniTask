using Scripts.Model;
using Scripts.Model.Data;
using Scripts.Model.Definitions;
using Scripts.Model.Definitions.Repositories.Items;
using Scripts.UI.Widgets;
using Scripts.Utilities.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Hud.QuickInventory
{
    public class InventoryItemWidget : MonoBehaviour, IItemRenderer<InventoryItemData>
    {
        [SerializeField] private Image icon;
        [SerializeField] private GameObject selection;
        [SerializeField] private Text value;

        private readonly CompositeDisposable trash = new CompositeDisposable();

        private int index;

        private void Start()
        {
            var session = FindObjectOfType<GameSession>();
            var index = session.QuickInventory.SelectedIndex;
            trash.Retain(index.SubscribeAndInvoke(OnIndexChanged));
        }

        private void OnIndexChanged(int newValue, int _)
        {
            selection.SetActive(index == newValue);
        }

        public void SetData(InventoryItemData item, int index)
        {
            this.index = index;
            var def = CoreGameConfigs.ConfigsInstance.Items.Get(item.Id);
            icon.sprite = def.Icon;
            value.text = def.HasTag(ItemTag.Stackable) ? item.Value.ToString() : string.Empty;
        }

        private void OnDestroy()
        {
            trash.Dispose();
        }
    }
}