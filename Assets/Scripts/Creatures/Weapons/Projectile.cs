using UnityEngine;

namespace Scripts.Creatures.Weapons
{
    public class Projectile : BaseProjectile
    {
        protected override void Start()
        {
            base.Start();

            var force = new Vector2(direction * speed, 0);
            rigBody.AddForce(force, ForceMode2D.Impulse);
        }
    }
}