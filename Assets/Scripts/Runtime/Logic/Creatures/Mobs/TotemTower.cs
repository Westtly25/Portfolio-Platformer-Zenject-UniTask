using System.Collections.Generic;
using System.Linq;
using Scripts.Components.Health;
using Scripts.Utilities;
using UnityEngine;

namespace Scripts.Creatures.Mobs
{
    public class TotemTower : MonoBehaviour
    {
        [SerializeField]
        private List<ShootingTrapAI> traps;
        [SerializeField]
        private Cooldown cooldown;

        private int _currentTrap;

        private void Start()
        {
            foreach (var shootingTrapAI in traps)
            {
                shootingTrapAI.enabled = false;
                var hp = shootingTrapAI.GetComponent<HealthComponent>();
                hp.onDie.AddListener(() => OnTrapDead(shootingTrapAI));
            }
        }

        private void OnTrapDead(ShootingTrapAI shootingTrapAI)
        {
            var index = traps.IndexOf(shootingTrapAI);
            traps.Remove(shootingTrapAI);
            if (index < _currentTrap)
            {
                _currentTrap--;
            }
        }

        private void Update()
        {
            if (traps.Count == 0)
            {
                enabled = false;
                Destroy(gameObject, 1f);
            }

            var hasAnyTarget = HasAnyTarget();
            if (hasAnyTarget)
            {
                if (cooldown.IsReady)
                {
                    traps[_currentTrap].Shoot();
                    cooldown.Reset();
                    _currentTrap = (int) Mathf.Repeat(_currentTrap + 1, traps.Count);
                }
            }
        }

        private bool HasAnyTarget()
        {
            foreach (var shootingTrapAI in traps)
            {
                if (shootingTrapAI.vision.IsTouchingLayer)
                    return true;
            }

            return false;
        }
    }
}