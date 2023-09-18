using Scripts.Utilities;
using UnityEngine;

namespace Scripts.Effects
{
    public class InfiniteBackground : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private Transform container;

        private Bounds containerBounds;
        private Bounds allBounds;

        private Vector3 boundsToTransformDelta;
        private Vector3 containerDelta;
        private Vector3 screenSize;

        private void Start()
        {
            var sprites = container.GetComponentsInChildren<SpriteRenderer>();
            containerBounds = sprites[0].bounds;

            foreach (var sprite in sprites)
            {
                containerBounds.Encapsulate(sprite.bounds);
            }

            allBounds = containerBounds;

            boundsToTransformDelta = transform.position - allBounds.center;
            containerDelta = container.position - allBounds.center;
        }

        private void LateUpdate()
        {
            var min = camera.ViewportToWorldPoint(Vector3.zero);
            var max = camera.ViewportToWorldPoint(Vector3.one);
            screenSize = new Vector3(max.x - min.x, max.y - min.y);

            allBounds.center = transform.position - boundsToTransformDelta;
            var camPosition = camera.transform.position.x;
            var screenLeft = new Vector3(camPosition - screenSize.x / 2, containerBounds.center.y);
            var screenRight = new Vector3(camPosition + screenSize.x / 2, containerBounds.center.y);

            if (!allBounds.Contains(screenLeft))
                InstantiateContainer(allBounds.min.x - containerBounds.extents.x);

            if (!allBounds.Contains(screenRight))
                InstantiateContainer(allBounds.max.x + containerBounds.extents.x);
        }

        private void InstantiateContainer(float boundCenterX)
        {
            var newBounds = new Bounds(new Vector3(boundCenterX, containerBounds.center.y), containerBounds.size);
            allBounds.Encapsulate(newBounds);

            boundsToTransformDelta = transform.position - allBounds.center;
            var newContainerXPos = boundCenterX + containerDelta.x;
            var newPosition = new Vector3(newContainerXPos, container.transform.position.y);

            Instantiate(container, newPosition, Quaternion.identity, transform);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected() =>
            GizmosUtilities.DrawBounds(allBounds, Color.magenta);
#endif
    }
}