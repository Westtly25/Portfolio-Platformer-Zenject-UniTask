using UnityEngine;
using UnityEngine.UI;
using Scripts.Model.Definitions;
using Scripts.Model.Definitions.Repositories;
using Scripts.Model.Definitions.Repositories.Items;

namespace Scripts.UI.Widgets
{
    public class ItemWidget : MonoBehaviour
    {
        [SerializeField]
        private Image icon;
        [SerializeField]
        private Text value;

        public void SetData(ItemWithCount price)
        {
            ItemConfig def = CoreGameConfigs.ConfigsInstance.Items.Get(price.ItemId);
            icon.sprite = def.Icon;
            value.text = price.Count.ToString();
        }
    }
}