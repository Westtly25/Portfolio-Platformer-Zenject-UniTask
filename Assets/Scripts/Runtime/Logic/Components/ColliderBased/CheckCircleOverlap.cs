using System;
using UnityEngine;
using System.Linq;
using Scripts.Utilities;
using UnityEngine.Events;

namespace Scripts.Components.ColliderBased
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField]
        private float radius = 1f;
        [SerializeField]
        private LayerMask mask;
        [SerializeField]
        private string[] tags;
        [SerializeField]
        private OnOverlapEvent onOverlap;

        private readonly Collider2D[] interactionResult = new Collider2D[10];

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = HandlesUtils.TransparentRed;
            UnityEditor.Handles.DrawSolidDisc(transform.position, Vector3.forward, radius);
        }
#endif
        public void Check()
        {
            int size = Physics2D.OverlapCircleNonAlloc(
                transform.position, radius, interactionResult, mask);

            for (var i = 0; i < size; i++)
            {
                var overlapResult = interactionResult[i];
                var isInTags = tags.Any(tag => overlapResult.CompareTag(tag));

                if (isInTags)
                    onOverlap?.Invoke(overlapResult.gameObject);
            }
        }

        [Serializable]
        public class OnOverlapEvent : UnityEvent<GameObject>
        {
        }
    }
}