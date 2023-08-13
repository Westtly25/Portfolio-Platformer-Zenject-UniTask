using UnityEngine;

namespace Scripts.Components.Movement
{
    public class VerticalLevitationComponent : MonoBehaviour
    {
        [SerializeField]
        private float frequency = 1f;
        [SerializeField]
        private float amplitude = 1f;
        [SerializeField]
        private bool randomize;

        private float originalY;
        private Rigidbody2D rigidbody;
        private float seed;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            originalY = rigidbody.position.y;
            if (randomize)
                seed = Random.value * Mathf.PI * 2;
        }

        private void Update()
        {
            var pos = rigidbody.position;
            pos.y = originalY + Mathf.Sin(seed + Time.time * frequency) * amplitude;
            rigidbody.MovePosition(pos);
        }
    }
}