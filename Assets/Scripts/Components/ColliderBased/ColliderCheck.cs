using UnityEngine;

namespace Scripts.Components.ColliderBased
{
    public class ColliderCheck : LayerCheck
    {
        private Collider2D colliderChecker;

        private void Awake() =>
            colliderChecker = GetComponent<Collider2D>();

        private void OnTriggerStay2D(Collider2D other) =>
            isTouchingLayer = colliderChecker.IsTouchingLayers(layer);

        private void OnTriggerExit2D(Collider2D other) =>
            isTouchingLayer = colliderChecker.IsTouchingLayers(layer);
    }
}