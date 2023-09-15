using UnityEngine;

namespace Scripts.Components.ColliderBased
{
    public class LineCheck : LayerCheck
    {
        [SerializeField] private Transform target;

        private readonly RaycastHit2D[] result = new RaycastHit2D[1];

        private void Update()
        {
            isTouchingLayer = Physics2D.LinecastNonAlloc(transform.position, target.position, result, layer) > 0;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.DrawLine(transform.position, target.position);
        }
#endif
    }
}