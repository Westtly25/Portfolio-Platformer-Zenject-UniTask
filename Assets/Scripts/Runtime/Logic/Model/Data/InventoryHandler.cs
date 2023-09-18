using System;
using Zenject;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using Scripts.Model.Definitions;
using System.Collections.Generic;
using Scripts.Model.Definitions.Player;
using Scripts.Model.Definitions.Repositories;
using Scripts.Model.Definitions.Repositories.Items;
using Assets.Scripts.Architecture.Services.Save_Service;
using Assets.Scripts.Architecture.Services.Save_Service.Interface;

namespace Scripts.Model.Data
{
    [Serializable]
    public class InventoryHandler : IInventoryHandler, IPersistentDataListener
    {
        [SerializeField]
        private List<InventoryItemData> inventory = new();

        public delegate void OnInventoryChanged(string id, int value);
        public OnInventoryChanged OnChanged;

        [Header("Configs")]
        private ItemsRepository itemsRepository;
        private PlayerConfigs playerConfigs;

        [Header("Injected")]
        private readonly GameConfigsProvider configsProvider;

        [Inject]
        public InventoryHandler(GameConfigsProvider configsProvider)
        {
            this.configsProvider = configsProvider;
        }

        public UniTaskVoid Initialize()
        {
            itemsRepository = configsProvider.CoreGameConfigs.Items;
            playerConfigs = configsProvider.CoreGameConfigs.Player;

            return new UniTaskVoid();
        }

        public void Add(string id, int value)
        {
            if (value <= 0) return;

            ItemConfig itemDef = configsProvider.CoreGameConfigs.Items.Get(id);
            if (itemDef.IsVoid) return;

            if (itemDef.HasTag(ItemTag.Stackable))
            {
                AddToStack(id, value);
            }
            else
            {
                AddNonStack(id, value);
            }

            OnChanged?.Invoke(id, Count(id));
        }

        public InventoryItemData[] GetAll(params ItemTag[] tags)
        {
            List<InventoryItemData> retValue = new List<InventoryItemData>();
            foreach (InventoryItemData item in inventory)
            {
                ItemConfig itemDef = configsProvider.CoreGameConfigs.Items.Get(item.Id);
                bool isAllRequirementsMet = tags.All(x => itemDef.HasTag(x));
                if (isAllRequirementsMet)
                    retValue.Add(item);
            }

            return retValue.ToArray();
        }

        private void AddToStack(string id, int value)
        {
            bool isFull = inventory.Count >= configsProvider.CoreGameConfigs.Player.InventorySize;
            InventoryItemData item = GetItem(id);
            if (item == null)
            {
                if (isFull) return;

                item = new InventoryItemData(id);
                inventory.Add(item);
            }

            item.Value += value;
        }

        private void AddNonStack(string id, int value)
        {
            int itemLasts = configsProvider.CoreGameConfigs.Player.InventorySize - inventory.Count;
            value = Mathf.Min(itemLasts, value);

            for (var i = 0; i < value; i++)
            {
                var item = new InventoryItemData(id) { Value = 1 };
                inventory.Add(item);
            }
        }

        public void Remove(string id, int value)
        {
            var itemDef = configsProvider.CoreGameConfigs.Items.Get(id);
            if (itemDef.IsVoid) return;

            if (itemDef.HasTag(ItemTag.Stackable))
            {
                RemoveFromStack(id, value);
            }
            else
            {
                RemoveNonStack(id, value);
            }

            OnChanged?.Invoke(id, Count(id));
        }

        private void RemoveFromStack(string id, int value)
        {
            InventoryItemData item = GetItem(id);
            if (item == null) return;

            item.Value -= value;

            if (item.Value <= 0)
                inventory.Remove(item);
        }

        private void RemoveNonStack(string id, int value)
        {
            for (int i = 0; i < value; i++)
            {
                var item = GetItem(id);
                if (item == null) return;

                inventory.Remove(item);
            }
        }

        private InventoryItemData GetItem(string id)
        {
            foreach (InventoryItemData itemData in inventory)
            {
                if (itemData.Id == id)
                    return itemData;
            }

            return null;
        }
        public int Count(string id)
        {
            int count = 0;
            foreach (InventoryItemData item in inventory)
            {
                if (item.Id == id)
                    count += item.Value;
            }

            return count;
        }

        public bool IsEnough(params ItemWithCount[] items)
        {
            Dictionary<string, int> joined = new Dictionary<string, int>();

            foreach (var item in items)
            {
                if (joined.ContainsKey(item.ItemId))
                    joined[item.ItemId] += item.Count;
                else
                    joined.Add(item.ItemId, item.Count);
            }

            foreach (KeyValuePair<string, int> kvp in joined)
            {
                int count = Count(kvp.Key);
                if (count < kvp.Value)
                    return false;
            }

            return true;
        }

        public void LoadData(GameData gameData)
        {
            inventory = gameData.Inventory;
        }

        public void SaveData(ref GameData gameData)
        {
            gameData.Inventory = inventory;
        }
    }
}