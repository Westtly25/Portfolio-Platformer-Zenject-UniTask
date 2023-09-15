using System.Collections;
using UnityEngine;

namespace Scripts.Components.ColliderBased
{
    [RequireComponent(typeof(Collider2D))]
    public class TmpDisableColliderComponent : MonoBehaviour
    {
        [SerializeField]
        private float disableTime = 0.3f;
        private Collider2D collider;
        private Coroutine coroutine;

        private void Awake()
        {
            collider = GetComponent<Collider2D>();
        }

        private void OnDisable()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
        }

        public void DisableCollider()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(DisableAndEnable());
        }

        private IEnumerator DisableAndEnable()
        {
            collider.enabled = false;
            yield return new WaitForSeconds(disableTime);
            collider.enabled = true;
        }
    }
}