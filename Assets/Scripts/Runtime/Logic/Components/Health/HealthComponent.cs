using System;
using Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField]
        private int health;
        [SerializeField]
        public UnityEvent onDamage;
        [SerializeField]
        private UnityEvent onHeal;
        [SerializeField]
        public UnityEvent onDie;
        [SerializeField]
        public HealthChangeEvent _onChange;
        private Lock immune = new Lock();

        public int Health => health;

        public Lock Immune => immune;

        public void ModifyHealth(int healthDelta)
        {
            if (healthDelta < 0 && Immune.IsLocked) return;
            if (health <= 0) return;

            health += healthDelta;
            _onChange?.Invoke(health);

            if (healthDelta < 0)
                onDamage?.Invoke();

            if (healthDelta > 0)
                onHeal?.Invoke();

            if (health <= 0)
                onDie?.Invoke();
        }

#if UNITY_EDITOR
        [ContextMenu("Update Health")]
        private void UpdateHealth()
        {
            _onChange?.Invoke(health);
        }
#endif

        public void SetHealth(int health) =>
            this.health = health;

        private void OnDestroy()
        {
            onDie.RemoveAllListeners();
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int>
        {
        }
    }
}