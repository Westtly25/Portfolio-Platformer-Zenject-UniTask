using UnityEngine;

namespace Scripts.UI
{
    public class AnimatedWindow : MonoBehaviour
    {
        private Animator animator;
        private static readonly int Show = Animator.StringToHash("Show");
        private static readonly int Hide = Animator.StringToHash("Hide");

        protected virtual void Start()
        {
            Debug.Log($"Hello from: {name}");
#if MY_AWESOME_DEFINE

            Debug.Log("Asesome define here!!!!");
#endif
            animator = GetComponent<Animator>();

            animator.SetTrigger(Show);
        }

        public void Close() =>
            animator.SetTrigger(Hide);

        public virtual void OnCloseAnimationComplete() =>
            Destroy(gameObject);
    }
}