using System.Collections;
using Scripts.Components.ColliderBased;
using Scripts.Components.GoBased;
using Scripts.Creatures.Mobs.Patrolling;
using UnityEngine;

namespace Scripts.Creatures.Mobs
{
    //TODO FSM needed
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private ColliderCheck vision;
        [SerializeField] private ColliderCheck canAttack;

        [SerializeField] private float alarmDelay = 0.5f;
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private float missHeroCooldown = 0.5f;

        [SerializeField] private float horizontalTreshold = 0.2f;

        private IEnumerator current;
        private GameObject target;

        private static readonly int IsDeadKey = Animator.StringToHash("is-dead");

        private SpawnListComponent particles;
        private Creature creature;
        private Animator animator;
        private bool isDead;
        private Patrol patrol;

        private void Awake()
        {
            particles = GetComponent<SpawnListComponent>();
            creature = GetComponent<Creature>();
            animator = GetComponent<Animator>();
            patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(patrol.DoPatrol());
        }

        public void OnHeroInVision(GameObject go)
        {
            if (isDead) return;

            RaycastHit2D[] cast = Physics2D.LinecastAll(transform.position, target.transform.position);
            
            target = go;
            StartState(AgroToHero());
        }

        private IEnumerator AgroToHero()
        {
            LookAtHero();
            particles.Spawn("Exclamation");
            yield return new WaitForSeconds(alarmDelay);

            StartState(GoToHero());
        }

        private void LookAtHero()
        {
            var direction = GetDirectionToTarget();
            creature.SetDirection(Vector2.zero);
            creature.UpdateSpriteDirection(direction);
        }

        private IEnumerator GoToHero()
        {
            while (vision.IsTouchingLayer)
            {
                if (canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    var horizontalDelta = Mathf.Abs(target.transform.position.x - transform.position.x);
                    if (horizontalDelta <= horizontalTreshold)
                        creature.SetDirection(Vector2.zero);
                    else
                        SetDirectionToTarget();
                }

                yield return null;
            }

            creature.SetDirection(Vector2.zero);
            particles.Spawn("MissHero");
            yield return new WaitForSeconds(missHeroCooldown);

            StartState(patrol.DoPatrol());
        }

        private IEnumerator Attack()
        {
            while (canAttack.IsTouchingLayer)
            {
                creature.Attack();
                yield return new WaitForSeconds(attackCooldown);
            }

            StartState(GoToHero());
        }

        private void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget();
            creature.SetDirection(direction);
        }

        private Vector2 GetDirectionToTarget()
        {
            var direction = target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }

        private void StartState(IEnumerator coroutine)
        {
            creature.SetDirection(Vector2.zero);

            if (current != null)
                StopCoroutine(current);

            current = coroutine;
            StartCoroutine(coroutine);
        }

        public void OnDie()
        {
            isDead = true;
            animator.SetBool(IsDeadKey, true);

            creature.SetDirection(Vector2.zero);
            if (current != null)
                StopCoroutine(current);
        }
    }
}