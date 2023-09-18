using UnityEngine;
using Scripts.Utilities;
using Scripts.Model.Data;
using Scripts.Model.Definitions;
using Scripts.Model.Definitions.Repositories.Items;

namespace Scripts.Components.Collectables
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryId, SerializeField]
        private string id;
        [SerializeField]
        private int count;

        public void Add(GameObject go)
        {
            ICanAddInInventory hero = go.GetInterface<ICanAddInInventory>();
            hero?.AddInInventory(id, count);
        }
    }
}