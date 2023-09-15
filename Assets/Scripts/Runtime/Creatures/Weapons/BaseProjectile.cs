using UnityEngine;

namespace Scripts.Creatures.Weapons
{
    public class BaseProjectile : MonoBehaviour
    {
        [SerializeField] protected float speed;
        [SerializeField] private bool invertX;

        protected Rigidbody2D rigBody;
        protected int direction;

        private void OnEnable() =>
            rigBody = rigBody != null ? rigBody : GetComponent<Rigidbody2D>();

        protected virtual void Start()
        {
            var mod = invertX ? -1 : 1;
            direction = mod * transform.lossyScale.x > 0 ? 1 : -1;
            rigBody = GetComponent<Rigidbody2D>();
        }
    }
}