using System;
using Scripts.Model.Definitions.Repositories.Items;

namespace Scripts.Model.Data
{
    [Serializable]
    public class InventoryItemData
    {
        [InventoryId]
        public string Id;
        public int Value;

        public InventoryItemData(string id) =>
            Id = id;
    }
}