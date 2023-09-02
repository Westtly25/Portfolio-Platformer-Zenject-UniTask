using UnityEngine;

namespace Scripts.Components.Interactions
{
    public class DoInteractionComponent : MonoBehaviour
    {
        public void DoInteraction(GameObject interacted)
        {
            if (interacted.TryGetComponent<InteractableComponent>(out InteractableComponent interactable))
                interactable.Interact();
        }
    }
}