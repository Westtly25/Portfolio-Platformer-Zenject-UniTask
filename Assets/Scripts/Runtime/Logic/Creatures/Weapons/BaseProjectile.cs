using UnityEngine;

namespace Scripts.Creatures.Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BaseProjectile : MonoBehaviour
    {
        [SerializeField, Range(0.01f, 20f)]
        protected float speed;
        [SerializeField]
        private bool invertX;

        protected Transform cachedTransforms;
        protected Rigidbody2D rigBody;
        protected int direction;

        private void Awake()
        {
            cachedTransforms = transform;
            rigBody = rigBody != null ? rigBody : GetComponent<Rigidbody2D>();
        }

        protected virtual void Start()
        {
            var mod = invertX ? -1 : 1;
            direction = mod * cachedTransforms.lossyScale.x > 0 ? 1 : -1;
            rigBody = GetComponent<Rigidbody2D>();
        }
    }
}