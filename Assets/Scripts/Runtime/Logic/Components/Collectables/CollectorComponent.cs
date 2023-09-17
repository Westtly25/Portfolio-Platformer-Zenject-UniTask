using UnityEngine;
using Scripts.Model;
using Scripts.Model.Data;
using System.Collections.Generic;
using Zenject;

namespace Scripts.Components.Collectables
{
    public class CollectorComponent : MonoBehaviour, ICanAddInInventory
    {
        [SerializeField]
        private List<InventoryItemData> items = new();

        private GameSession gameSession;

        [Inject]
        public void Constructor(GameSession gameSession) =>
            this.gameSession = gameSession;

        public void AddInInventory(string id, int value) =>
            items.Add(new InventoryItemData(id) {Value = value});

        public void DropInInventory()
        {
            var session = FindObjectOfType<GameSession>();

            foreach (var inventoryItemData in items)
                session.Data.Inventory.Add(inventoryItemData.Id, inventoryItemData.Value);

            items.Clear();
        }
    }
}