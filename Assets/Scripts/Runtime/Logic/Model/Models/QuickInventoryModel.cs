using System;
using UnityEngine;
using Scripts.Utilities.Disposables;
using Scripts.Model.Definitions;
using Scripts.Model.Data.Properties;
using Scripts.Model.Definitions.Repositories.Items;

namespace Scripts.Model.Data
{
    public class QuickInventoryModel : IDisposable
    {
        private readonly PlayerData data;

        public InventoryItemData[] Inventory { get; private set; }

        public readonly IntProperty SelectedIndex = new IntProperty();

        public event Action OnChanged;

        public InventoryItemData SelectedItem
        {
            get
            {
                if (Inventory.Length > 0 && Inventory.Length > SelectedIndex.Value)
                    return Inventory[SelectedIndex.Value];

                return null;
            }
        }

        public ItemConfig SelectedDef => CoreGameConfigs.ConfigsInstance.Items.Get(SelectedItem?.Id);

        public QuickInventoryModel(PlayerData data)
        {
            this.data = data;

            Inventory = this.data.Inventory.GetAll(ItemTag.Usable);
            this.data.Inventory.OnChanged += OnChangedInventory;
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        private void OnChangedInventory(string id, int value)
        {
            Inventory = data.Inventory.GetAll(ItemTag.Usable);
            SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
            OnChanged?.Invoke();
        }

        public void SetNextItem() =>
            SelectedIndex.Value = (int) Mathf.Repeat(SelectedIndex.Value + 1, Inventory.Length);

        public void Dispose()
        {
            data.Inventory.OnChanged -= OnChangedInventory;
        }
    }
}