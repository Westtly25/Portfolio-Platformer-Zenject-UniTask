using UnityEngine;
using Scripts.Utilities;
using Scripts.Animations;
using Scripts.Components.ColliderBased;

namespace Scripts.Creatures.Mobs
{
    public class ShootingTrapAI : MonoBehaviour
    {
        [SerializeField] public ColliderCheck vision;
        [SerializeField] private Cooldown cooldown;
        [SerializeField] private SpriteAnimation animation;

        private void Update()
        {
            if (vision.IsTouchingLayer && cooldown.IsReady)
                Shoot();
        }

        public void Shoot()
        {
            cooldown.Reset();
            animation.SetClip("start-attack");
        }
    }
}