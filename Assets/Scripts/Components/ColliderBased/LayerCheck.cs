using UnityEngine;

namespace Scripts.Components.ColliderBased
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField]
        protected LayerMask layer;

        [SerializeField]
        protected bool isTouchingLayer;

        public bool IsTouchingLayer => isTouchingLayer;
    }
}