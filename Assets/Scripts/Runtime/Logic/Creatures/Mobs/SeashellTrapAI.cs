using UnityEngine;
using Scripts.Utilities;
using Scripts.Components.GoBased;
using Scripts.Components.ColliderBased;

namespace Scripts.Creatures.Mobs
{
    public class SeashellTrapAI : MonoBehaviour
    {
        [SerializeField] private ColliderCheck vision;

        [Header("Melee")] 
        [SerializeField] 
        private Cooldown meleeCooldown;
        [SerializeField] 
        private CheckCircleOverlap meleeAttack;
        [SerializeField] 
        private ColliderCheck meleeCanAttack;
        [Header("Range")]
        [SerializeField]
        private Cooldown rangeCooldown;
        [SerializeField] private SpawnComponent rangeAttack;

        private static readonly int Melee = Animator.StringToHash("melee");
        private static readonly int Range = Animator.StringToHash("range");

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (vision.IsTouchingLayer)
            {
                if (meleeCanAttack.IsTouchingLayer)
                {
                    if (meleeCooldown.IsReady)
                        MeleeAttack();
                    return;
                }

                if (rangeCooldown.IsReady)
                    RangeAttack();
            }
        }

        private void RangeAttack()
        {
            rangeCooldown.Reset();
            animator.SetTrigger(Range);
        }

        private void MeleeAttack()
        {
            meleeCooldown.Reset();
            animator.SetTrigger(Melee);
        }

        public void OnMeleeAttack()
        {
            meleeAttack.Check();
        }

        public void OnRangeAttack()
        {
            rangeAttack.Spawn();
        }
    }
}