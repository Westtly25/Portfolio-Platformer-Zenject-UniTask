using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Components.Interactions
{
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent action;

        public void Interact() =>
            action?.Invoke();
    }
}