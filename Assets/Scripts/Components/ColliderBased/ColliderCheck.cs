using UnityEngine;

namespace Scripts.Components.ColliderBased
{
    public class ColliderCheck : LayerCheck
    {
        private Collider2D collider;

        private void Awake() =>
            collider = GetComponent<Collider2D>();

        private void OnTriggerStay2D(Collider2D other)
        {
            isTouchingLayer = collider.IsTouchingLayers(layer);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            isTouchingLayer = collider.IsTouchingLayers(layer);
        }
    }
}