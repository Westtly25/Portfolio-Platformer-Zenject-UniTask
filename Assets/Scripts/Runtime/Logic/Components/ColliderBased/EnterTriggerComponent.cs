using Scripts.Utilities;
using UnityEngine;

namespace Scripts.Components.ColliderBased
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string tag;
        [SerializeField] private LayerMask layer = ~0;
        [SerializeField] private EnterEvent action;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.IsInLayer(layer))
                return;

            if (!string.IsNullOrEmpty(tag) && !other.gameObject.CompareTag(tag))
                return;
            
            action?.Invoke(other.gameObject);
        }
    }
}