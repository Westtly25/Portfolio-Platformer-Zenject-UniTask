using UnityEngine;
using Scripts.Utilities;
using System.Collections;
using Scripts.Model.Data;
using UnityEngine.Events;

namespace Scripts.UI.Hud.Dialogs
{
    public class DialogBoxController : MonoBehaviour
    {
        [SerializeField] private GameObject container;
        [SerializeField] private Animator animator;

        [Space] [SerializeField] private float textSpeed = 0.09f;

        [Header("sounds")] [SerializeField] private AudioClip typing;
        [SerializeField] private AudioClip open;
        [SerializeField] private AudioClip close;

        [Space] [SerializeField] protected DialogContent content;

        private static readonly int IsOpen = Animator.StringToHash("IsOpen");

        private DialogData data;
        private int currentSentence;
        private AudioSource sfxSource;
        private Coroutine typingRoutine;
        private UnityEvent onComplete;

        protected Sentence CurrentSentence => data.Sentences[currentSentence];

        private void Start()
        {
            sfxSource = AudioUtils.FindSfxSource();
        }

        public void ShowDialog(DialogData data, UnityEvent onComplete)
        {
            this.onComplete = onComplete;
            this.data = data;
            currentSentence = 0;
            CurrentContent.Text.text = string.Empty;

            container.SetActive(true);
            sfxSource.PlayOneShot(open);
            animator.SetBool(IsOpen, true);
        }

        private IEnumerator TypeDialogText()
        {
            CurrentContent.Text.text = string.Empty;
            var sentence = CurrentSentence;
            CurrentContent.TrySetIcon(sentence.Icon);

            //TODO
            var localizedSentence = ""; /*= sentence.Value.Localize();*/

            foreach (var letter in localizedSentence)
            {
                CurrentContent.Text.text += letter;
                sfxSource.PlayOneShot(typing);
                yield return new WaitForSeconds(textSpeed);
            }

            typingRoutine = null;
        }

        protected virtual DialogContent CurrentContent => content;

        public void OnSkip()
        {
            if (typingRoutine == null) return;

            StopTypeAnimation();
            var sentence = data.Sentences[currentSentence].Value;
            CurrentContent.Text.text = ""; //sentence.Localize();
        }

        public void OnContinue()
        {
            StopTypeAnimation();
            currentSentence++;

            var isDialogCompleted = currentSentence >= data.Sentences.Length;
            if (isDialogCompleted)
            {
                HideDialogBox();
                onComplete?.Invoke();
            }
            else
            {
                OnStartDialogAnimation();
            }
        }

        private void HideDialogBox()
        {
            animator.SetBool(IsOpen, false);
            sfxSource.PlayOneShot(close);
        }

        private void StopTypeAnimation()
        {
            if (typingRoutine != null)
                StopCoroutine(typingRoutine);
            typingRoutine = null;
        }

        protected virtual void OnStartDialogAnimation()
        {
            typingRoutine = StartCoroutine(TypeDialogText());
        }

        private void OnCloseAnimationComplete()
        {
        }
    }
}