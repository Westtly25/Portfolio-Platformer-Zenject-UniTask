using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Widgets
{
    public class ProgressBarWidget : MonoBehaviour
    {
        [SerializeField]
        private Image bar;

        public void SetProgress(float progress) =>
            bar.fillAmount = progress;
    }
}