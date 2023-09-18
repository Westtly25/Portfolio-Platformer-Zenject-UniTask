using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Scripts.Components
{
    public class ShowTargetComponent : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float delay = 0.5f;
        [SerializeField] private UnityEvent onDelay;

        [SerializeField] private ShowTargetController controller;

        private Coroutine coroutine;

        private void OnValidate()
        {
            if (controller == null)
            {
                controller = FindObjectOfType<ShowTargetController>();
            }
        }

        public void Play()
        {
            controller.SetPosition(target.position);
            controller.SetState(true);

            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(WaitAndReturn());
        }

        private IEnumerator WaitAndReturn()
        {
            yield return new WaitForSeconds(delay);

            onDelay?.Invoke();
            controller.SetState(false);
        }
    }
}