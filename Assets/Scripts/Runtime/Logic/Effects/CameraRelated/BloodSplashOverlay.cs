using UnityEngine;
using Scripts.Model;
using Scripts.Utilities.Disposables;
using Scripts.Model.Definitions.Player;

namespace Scripts.Effects.CameraRelated
{
    [RequireComponent(typeof(Animator))]
    public class BloodSplashOverlay : MonoBehaviour
    {
        [SerializeField]
        private Transform overlay;

        private static readonly int Health = Animator.StringToHash("Health");

        private Animator animator;
        private Vector3 overScale;
        private GameSession session;

        private readonly CompositeDisposable trash = new CompositeDisposable();

        private void Start()
        {
            animator = GetComponent<Animator>();
            overScale = overlay.localScale - Vector3.one;

            session = FindObjectOfType<GameSession>();
            trash.Retain(session.Data.Hp.SubscribeAndInvoke(OnHpChanged));
        }

        private void OnHpChanged(int newValue, int _)
        {
            var maxHp = session.StatsModel.GetValue(StatId.Hp);
            var hpNormalized = newValue / maxHp;
            animator.SetFloat(Health, hpNormalized);

            var overlayModifier = Mathf.Max(hpNormalized - 0.3f, 0f);
            overlay.localScale = Vector3.one + overScale * overlayModifier;
        }

        private void OnDestroy() =>
            trash.Dispose();
    }
}