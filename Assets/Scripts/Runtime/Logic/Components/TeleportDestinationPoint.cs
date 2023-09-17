using UnityEngine;

namespace Scripts.Components
{
    public class TeleportDestinationPoint : MonoBehaviour
    {
        [SerializeField]
        private Transform cachedTransform;

        public Transform CachedTransform => cachedTransform;

        private void Awake() =>
            cachedTransform = transform;
    }
}