using UnityEngine;

namespace Scripts.Components
{
    public class SwitchComponent : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private bool state;
        [SerializeField]
        private string animationKey;
        [SerializeField]
        private bool updateOnStart;

        private void Start()
        {
            if(updateOnStart)
                animator.SetBool(animationKey, state);
        }

        public void Switch()
        {
            state = !state;
            animator.SetBool(animationKey, state);
        }

        [ContextMenu("Switch")]
        public void SwitchIt()
        {
            Switch();
        }
    }
}