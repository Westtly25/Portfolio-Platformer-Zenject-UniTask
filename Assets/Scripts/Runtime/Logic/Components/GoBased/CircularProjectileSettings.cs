using System;
using Scripts.Creatures.Weapons;
using UnityEngine;

namespace Scripts.Components.GoBased
{
    [Serializable]
    public struct CircularProjectileSettings
    {
        [SerializeField]
        private DirectionalProjectile prefab;
        [SerializeField]
        private int burstCount;
        [SerializeField]
        private float delay;

        public DirectionalProjectile Prefab => prefab;
        public int BurstCount => burstCount;
        public float Delay => delay;
    }
}