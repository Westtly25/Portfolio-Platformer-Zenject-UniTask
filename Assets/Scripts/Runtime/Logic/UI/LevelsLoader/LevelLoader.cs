using UnityEngine;
using System.Collections;

namespace Scripts.UI.LevelsLoader
{
    [RequireComponent (typeof (LevelLoader))]
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField, Min(1f)]
        private float transitionTime = 10f;

        private static readonly int Enabled = Animator.StringToHash("Enabled");

        private void Awake() =>
            PlayAnimation();

        public void PlayAnimation() =>
            StartCoroutine(StartAnimation());

        private IEnumerator StartAnimation()
        {
            animator.SetBool(Enabled, true);
            yield return new WaitForSeconds(transitionTime);
            animator.SetBool(Enabled, false);
        }
    }
}