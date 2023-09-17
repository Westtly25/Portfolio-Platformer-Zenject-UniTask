using System;
using Scripts.Model.Definitions.Repositories.Items;
using UnityEngine;

namespace Scripts.Model.Definitions.Repositories
{
    [Serializable]
    public struct ItemWithCount
    {
        [InventoryId, SerializeField]
        private string itemId;
        [SerializeField]
        private int count;

        public string ItemId => itemId;
        public int Count => count;
    }
}