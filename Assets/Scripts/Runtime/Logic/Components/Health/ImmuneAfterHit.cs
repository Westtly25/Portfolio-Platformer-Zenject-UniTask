using System.Collections;
using Scripts.Utilities.Disposables;
using UnityEngine;

namespace Scripts.Components.Health
{
    [RequireComponent(typeof(HealthComponent))]
    public class ImmuneAfterHit : MonoBehaviour
    {
        [SerializeField]
        private float immuneTime;

        private HealthComponent health;
        private readonly CompositeDisposable trash = new CompositeDisposable();
        private Coroutine coroutine;

        private void Awake()
        {
            health = GetComponent<HealthComponent>();
            trash.Retain(health.onDamage.Subscribe(OnDamage));
        }

        private void OnDamage()
        {
            TryStop();
            if (immuneTime > 0)
                coroutine = StartCoroutine(MakeImmune());
        }

        private void TryStop()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = null;
        }

        private IEnumerator MakeImmune()
        {
            health.Immune.Retain(this);
            yield return new WaitForSeconds(immuneTime);
            health.Immune.Release(this);
        }

        private void OnDestroy()
        {
            TryStop();
            trash.Dispose();
        }
    }
}