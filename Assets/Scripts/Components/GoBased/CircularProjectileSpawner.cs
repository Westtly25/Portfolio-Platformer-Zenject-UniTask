using System;
using System.Collections;
using Scripts.Creatures.Weapons;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Components.GoBased
{
    public class CircularProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private CircularProjectileSettings[] settings;

        public int Stage { get; set; }

        [ContextMenu("Launch!")]
        public void LaunchProjectiles() =>
            StartCoroutine(SpawnProjectiles());

        private IEnumerator SpawnProjectiles()
        {
            var setting = settings[Stage];
            var sectorStep = 2 * Mathf.PI / setting.BurstCount;

            for (int i = 0; i < setting.BurstCount; i++)
            {
                var angle = sectorStep * i;
                var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                GameObject instance = SpawnUtils.Spawn(setting.Prefab.gameObject, transform.position);
                DirectionalProjectile projectile = instance.GetComponent<DirectionalProjectile>();
                projectile.Launch(direction);

                yield return new WaitForSeconds(setting.Delay);
            }
        }
    }

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