using UnityEngine;

namespace Scripts.Components.Movement
{
    public class CircularMovement : MonoBehaviour
    {
        [SerializeField] private float radius = 1f;
        [SerializeField] private float speed = 1f;
        private Rigidbody2D[] bodies;
        private Vector2[] positions;
        private float time;

        private void Awake() =>
            UpdateContent();

        private void UpdateContent()
        {
            bodies = GetComponentsInChildren<Rigidbody2D>();
            positions = new Vector2[bodies.Length];
        }

        private void Update()
        {
            CalculatePositions();
            var isAllDead = true;
            for (var i = 0; i < bodies.Length; i++)
            {
                if (bodies[i])
                {
                    bodies[i].MovePosition(positions[i]);
                    isAllDead = false;
                }
            }

            if (isAllDead)
            {
                enabled = false;
                Destroy(gameObject, 1f);
            }

            time += Time.deltaTime;
        }

        private void CalculatePositions()
        {
            var step = 2 * Mathf.PI / bodies.Length;

            Vector2 containerPosition = transform.position;
            for (var i = 0; i < bodies.Length; i++)
            {
                var angle = step * i;
                var pos = new Vector2(
                    Mathf.Cos(angle + time * speed) * radius,
                    Mathf.Sin(angle + time * speed) * radius
                );
                positions[i] = containerPosition + pos;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateContent();
            CalculatePositions();
            for (var i = 0; i < bodies.Length; i++)
            {
                bodies[i].transform.position = positions[i];
            }
        }

        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
        }
#endif
    }
}