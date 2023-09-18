using Scripts.Components.Health;
using Scripts.Utilities;
using UnityEngine;

namespace Scripts.Components
{
    public class HeroShield : MonoBehaviour
    {
        [SerializeField] private HealthComponent health;
        [SerializeField] private Cooldown cooldown;

        public void Use()
        {
            health.Immune.Retain(this);
            cooldown.Reset();
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (cooldown.IsReady)
                gameObject.SetActive(false);
        }

        private void OnDisable() =>
            health.Immune.Release(this);
    }
}