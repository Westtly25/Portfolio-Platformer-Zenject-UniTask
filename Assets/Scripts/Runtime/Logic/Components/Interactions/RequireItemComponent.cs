using Scripts.Model;
using Scripts.Model.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Components.Interactions
{
    public class RequireItemComponent : MonoBehaviour
    {
        [SerializeField] private InventoryItemData[] required;
        [SerializeField] private bool removeAfterUse;

        [SerializeField] private UnityEvent onSuccess;
        [SerializeField] private UnityEvent onFail;

        public void Check()
        {
            var session = FindObjectOfType<GameSession>();
            var areAllRequirementsMet = true;

            foreach (var item in required)
            {
                var numItems = session.Data.Inventory.Count(item.Id);
                if (numItems < item.Value)
                    areAllRequirementsMet = false;
            }

            if (areAllRequirementsMet)
            {
                if (removeAfterUse)
                {
                    foreach (var item in required)
                        session.Data.Inventory.Remove(item.Id, item.Value);
                }

                onSuccess?.Invoke();
            }
            else
            {
                onFail?.Invoke();
            }
        }
    }
}