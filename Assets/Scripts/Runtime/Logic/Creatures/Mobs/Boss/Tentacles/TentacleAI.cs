using System;
using UnityEngine;
using Scripts.Creatures.Mobs.Patrolling;

namespace Scripts.Creatures.Mobs.Boss.Tentacles
{
    public class TentacleAI : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Vector2 direction;
        [SerializeField] private Rigidbody2D rigb;
        [SerializeField] private Patrol patrol;

        private void Start() =>
            StartCoroutine(patrol.DoPatrol());

        public void SetDirection(Vector2 direction) =>
            this.direction = direction;

        private void FixedUpdate() =>
            rigb.velocity = direction * speed;

        private void Update()
        {
            if (direction.x > 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (direction.x < 0)
                transform.localScale = new Vector3(1, 1, 1);
        }
    }
}