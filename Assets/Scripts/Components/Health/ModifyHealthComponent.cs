using UnityEngine;

namespace Scripts.Components.Health
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField]
        private int hpDelta;

        public void SetDelta(int delta) =>
            hpDelta = delta;

        public void Apply(GameObject target)
        {
            target.TryGetComponent<HealthComponent>(out HealthComponent healthComponent);

            if (healthComponent != null)
                healthComponent.ModifyHealth(hpDelta);
        }
    }
}