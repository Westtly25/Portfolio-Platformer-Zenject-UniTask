using UnityEngine;
using Scripts.Components.Audio;
using Scripts.Components.GoBased;
using Scripts.Components.ColliderBased;

namespace Scripts.Creatures
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlaySoundsComponent))]
    public class Creature : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField]
        private bool invertScale;
        [SerializeField]
        private float speed;
        [SerializeField]
        protected float jumpSpeed;
        [SerializeField]
        private float damageVelocity;

        [Header("Checkers")]
        [SerializeField]
        protected LayerMask groundLayer;
        [SerializeField]
        private ColliderCheck groundCheck;
        [SerializeField]
        private CheckCircleOverlap attackRange;
        [SerializeField]
        protected SpawnListComponent particles;

        protected Rigidbody2D rigidbody;
        protected Vector2 direction;
        protected Animator animator;
        protected PlaySoundsComponent sounds;
        protected bool isGrounded;
        private bool isJumping;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            sounds = GetComponent<PlaySoundsComponent>();
        }

        public void SetDirection(Vector2 direction) =>
            this.direction = direction;

        protected virtual void Update()
        {
            isGrounded = groundCheck.IsTouchingLayer;
        }

        private void FixedUpdate()
        {
            var xVelocity = CalculateXVelocity();
            var yVelocity = CalculateYVelocity();
            rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            animator.SetBool(IsGroundKey, isGrounded);
            animator.SetBool(IsRunning, direction.x != 0);
            animator.SetFloat(VerticalVelocity, rigidbody.velocity.y);

            UpdateSpriteDirection(direction);
        }

        protected virtual float CalculateXVelocity() =>
            direction.x * CalculateSpeed();

        protected virtual float CalculateSpeed() =>
             speed;

        protected virtual float CalculateYVelocity()
        {
            var yVelocity = rigidbody.velocity.y;
            var isJumpPressing = direction.y > 0;

            if (isGrounded)
            {
                isJumping = false;
            }

            if (isJumpPressing)
            {
                isJumping = true;

                var isFalling = rigidbody.velocity.y <= 0.001f;
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
            }
            else if (rigidbody.velocity.y > 0 && isJumping)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {
            if (isGrounded)
            {
                yVelocity = jumpSpeed;
                DoJumpVfx();
            }

            return yVelocity;
        }

        protected void DoJumpVfx()
        {
            particles.Spawn("Jump");
            sounds.Play("Jump");
        }

        public void UpdateSpriteDirection(Vector2 direction)
        {
            var multiplier = invertScale ? -1 : 1;
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(multiplier, 1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1 * multiplier, 1, 1);
            }
        }

        public virtual void TakeDamage()
        {
            isJumping = false;
            animator.SetTrigger(Hit);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, damageVelocity);
        }

        public virtual void Attack()
        {
            animator.SetTrigger(AttackKey);
            sounds.Play("Melee");
        }

        public void OnDoAttack()
        {
            attackRange.Check();
            particles.Spawn("Slash");
        }
    }
}