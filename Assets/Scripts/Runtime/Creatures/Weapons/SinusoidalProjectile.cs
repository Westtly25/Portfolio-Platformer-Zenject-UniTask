using UnityEngine;

namespace Scripts.Creatures.Weapons
{
    public class SinusoidalProjectile : BaseProjectile
    {
        [SerializeField]
        private float frequency = 1f;
        [SerializeField]
        private float amplitude = 1f;

        private float originalY;
        private float time;

        protected override void Start()
        {
            base.Start();
            originalY = rigBody.position.y;
        }

        private void FixedUpdate()
        {
            Vector2 position = rigBody.position;
            position.x += direction * speed;
            position.y = originalY + Mathf.Sin(time * frequency) * amplitude;
            rigBody.MovePosition(position);
            time += Time.fixedDeltaTime;
        }
    }
}