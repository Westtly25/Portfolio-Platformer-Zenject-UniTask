using UnityEngine;

namespace Scripts.Creatures.Weapons
{
    public class DirectionalProjectile : BaseProjectile
    {
        public void Launch(Vector2 direction) =>
            rigBody.AddForce(direction * speed, ForceMode2D.Impulse);
    }
}