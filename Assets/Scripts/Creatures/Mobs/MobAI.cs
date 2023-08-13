using System.Collections;
using Scripts.Components.ColliderBased;
using Scripts.Components.GoBased;
using Scripts.Creatures.Mobs.Patrolling;
using UnityEngine;

namespace Scripts.Creatures.Mobs
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private ColliderCheck vision;
        [SerializeField] private ColliderCheck canAttack;

        [SerializeField] private float alarmDelay = 0.5f;
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private float missHeroCooldown = 0.5f;

        [SerializeField] private float horizontalTreshold = 0.2f;

        private IEnumerator _current;
        private GameObject _target;

        private static readonly int IsDeadKey = Animator.StringToHash("is-dead");

        private SpawnListComponent _particles;
        private Creature _creature;
        private Animator _animator;
        private bool _isDead;
        private Patrol _patrol;

        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }

        public void OnHeroInVision(GameObject go)
        {
            if (_isDead) return;

            // smth to do:
            var cast = Physics2D.LinecastAll(transform.position, _target.transform.position);
            
            _target = go;
            StartState(AgroToHero());
        }

        private IEnumerator AgroToHero()
        {
            LookAtHero();
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(alarmDelay);

            StartState(GoToHero());
        }

        private void LookAtHero()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(Vector2.zero);
            _creature.UpdateSpriteDirection(direction);
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
                    var horizontalDelta = Mathf.Abs(_target.transform.position.x - transform.position.x);
                    if (horizontalDelta <= horizontalTreshold)
                        _creature.SetDirection(Vector2.zero);
                    else
                        SetDirectionToTarget();
                }

                yield return null;
            }

            _creature.SetDirection(Vector2.zero);
            _particles.Spawn("MissHero");
            yield return new WaitForSeconds(missHeroCooldown);

            StartState(_patrol.DoPatrol());
        }

        private IEnumerator Attack()
        {
            while (canAttack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(attackCooldown);
            }

            StartState(GoToHero());
        }

        private void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget();
            _creature.SetDirection(direction);
        }

        private Vector2 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }

        private void StartState(IEnumerator coroutine)
        {
            _creature.SetDirection(Vector2.zero);

            if (_current != null)
                StopCoroutine(_current);

            _current = coroutine;
            StartCoroutine(coroutine);
        }

        public void OnDie()
        {
            _isDead = true;
            _animator.SetBool(IsDeadKey, true);

            _creature.SetDirection(Vector2.zero);
            if (_current != null)
                StopCoroutine(_current);
        }
    }
}