using System.Collections.Generic;
using Scripts.Model;
using Scripts.Model.Data;
using Scripts.UI.Widgets;
using Scripts.Utilities.Disposables;
using UnityEngine;

namespace Scripts.UI.Hud.QuickInventory
{
    public class QuickInventoryController : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private InventoryItemWidget prefab;

        private readonly CompositeDisposable trash = new CompositeDisposable();

        private GameSession session;
        private List<InventoryItemWidget> _createdItem = new List<InventoryItemWidget>();

        private DataGroup<InventoryItemData, InventoryItemWidget> _dataGroup;

        private void Start()
        {
            _dataGroup = new DataGroup<InventoryItemData, InventoryItemWidget>(prefab, container);
            session = FindObjectOfType<GameSession>();
            trash.Retain(session.QuickInventory.Subscribe(Rebuild));
            Rebuild();
        }

        private void Rebuild()
        {
            var inventory = session.QuickInventory.Inventory;
            _dataGroup.SetData(inventory);
        }

        private void OnDestroy()
        {
            trash.Dispose();
        }
    }
}