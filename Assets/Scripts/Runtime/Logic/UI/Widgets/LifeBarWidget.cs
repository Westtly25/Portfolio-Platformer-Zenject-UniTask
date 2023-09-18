using Scripts.Components.Health;
using Scripts.Utilities.Disposables;
using UnityEngine;

namespace Scripts.UI.Widgets
{
    public class LifeBarWidget : MonoBehaviour
    {
        [SerializeField]
        private ProgressBarWidget lifeBar;
        [SerializeField]
        private HealthComponent hp;

        private readonly CompositeDisposable trash = new CompositeDisposable();
        private int maxHp;

        private void Start()
        {
            if (hp == null)
                hp = GetComponentInParent<HealthComponent>();

            maxHp = hp.Health;

            trash.Retain(hp.onDie.Subscribe(OnDie));
            trash.Retain(hp._onChange.Subscribe(OnHpChanged));
        }

        private void OnDie()
        {
            Destroy(gameObject);
        }

        private void OnHpChanged(int hp)
        {
            var progress = (float) hp / maxHp;
            lifeBar.SetProgress(progress);
        }

        private void OnDestroy()
        {
            trash.Dispose();
        }
    }
}