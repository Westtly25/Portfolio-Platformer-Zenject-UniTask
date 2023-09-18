using Scripts.Components.Health;
using Scripts.Utilities;
using Scripts.Utilities.Disposables;
using UnityEngine;

namespace Scripts.UI.Widgets
{
    public class BossHpWidget : MonoBehaviour
    {
        [SerializeField]
        private HealthComponent health;
        [SerializeField]
        private ProgressBarWidget hpBar;
        [SerializeField]
        private CanvasGroup canvas;

        private readonly CompositeDisposable trash = new CompositeDisposable();
        private float maxHealth;

        private void Start()
        {
            maxHealth = health.Health;
            trash.Retain(health._onChange.Subscribe(OnHpChanged));
            trash.Retain(health.onDie.Subscribe(HideUI));
        }

        [ContextMenu("Show")]
        public void ShowUI()
        {
            this.LerpAnimated(0, 1, 1, SetAlpha);
        }

        private void SetAlpha(float alpha)
        {
            canvas.alpha = alpha;
        }

        [ContextMenu("Hide")]
        private void HideUI()
        {
            this.LerpAnimated(1, 0, 1, SetAlpha);
        }

        private void OnHpChanged(int hp)
        {
            hpBar.SetProgress(hp / maxHealth);
        }

        private void OnDestroy()
        {
            trash.Dispose();
        }
    }
}