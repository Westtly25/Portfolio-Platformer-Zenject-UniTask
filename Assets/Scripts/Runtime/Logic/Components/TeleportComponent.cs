using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

namespace Scripts.Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField]
        private TeleportDestinationPoint destinationPoint;
        [SerializeField]
        private float alphaTime = 1;
        [SerializeField]
        private float moveTime = 1;

        public void Teleport(GameObject target) =>
            StartCoroutine(AnimateTeleport(target));

        private IEnumerator AnimateTeleport(GameObject target)
        {
            SpriteRenderer sprite = null;
            PlayerInput input = null;

            if (target.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
                sprite = spriteRenderer;

            if (target.TryGetComponent<PlayerInput>(out PlayerInput playerInput))
                input = playerInput;

            SetLockInput(input, true);

            yield return AlphaAnimation(sprite, 0);
            target.SetActive(false);

            yield return MoveAnimation(target);

            target.SetActive(true);
            yield return AlphaAnimation(sprite, 1);
            SetLockInput(input, false);
        }

        private void SetLockInput(PlayerInput input, bool isLocked)
        {
            if (input != null)
                input.enabled = !isLocked;
        }

        private IEnumerator MoveAnimation(GameObject target)
        {
            var moveTime = 0f;
            while (moveTime < this.moveTime)
            {
                moveTime += Time.deltaTime;
                var progress = moveTime / this.moveTime;
                target.transform.position = Vector3.Lerp(target.transform.position, destinationPoint.CachedTransform.position, progress);

                yield return null;
            }
        }

        private IEnumerator AlphaAnimation(SpriteRenderer sprite, float destAlpha)
        {
            var time = 0f;
            var spriteAlpha = sprite.color.a;
            while (time < alphaTime)
            {
                time += Time.deltaTime;
                float progress = time / alphaTime;
                float tmpAlpha = Mathf.Lerp(spriteAlpha, destAlpha, progress);
                Color color = sprite.color;
                color.a = tmpAlpha;
                sprite.color = color;

                yield return null;
            }
        }
    }
}