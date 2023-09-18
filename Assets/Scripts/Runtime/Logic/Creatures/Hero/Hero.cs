using UnityEngine;
using Scripts.Model;
using Scripts.Utilities;
using System.Collections;
using Scripts.Components;
using Scripts.Model.Data;
using Scripts.Model.Definitions;
using Scripts.Components.Health;
using Scripts.Components.GoBased;
using Scripts.Effects.CameraRelated;
using Scripts.Creatures.Hero.Features;
using Scripts.Model.Definitions.Player;
using Scripts.Components.ColliderBased;
using Scripts.Model.Definitions.Repositories;
using Scripts.Model.Definitions.Repositories.Items;

namespace Scripts.Creatures.Hero
{
    //Refactoring Needed
    public class Hero : Creature, ICanAddInInventory
    {
        [SerializeField]
        private CheckCircleOverlap interactionCheck;
        [SerializeField]
        private ColliderCheck wallCheck;

        [SerializeField]
        private float slamDownVelocity;
        [SerializeField]
        private Cooldown throwCooldown;
        [SerializeField]
        private RuntimeAnimatorController armed;
        [SerializeField]
        private RuntimeAnimatorController disarmed;

        [Header("Super throw")]
        [SerializeField]
        private Cooldown superThrowCooldown;

        [SerializeField]
        private int superThrowParticles;
        [SerializeField]
        private float superThrowDelay;
        [SerializeField]
        private ProbabilityDropComponent hitDrop;
        [SerializeField]
        private SpawnComponent throwSpawner;
        [SerializeField]
        private HeroShield shield;
        [SerializeField]
        private HeroFlashlight flashlight;

        private static readonly int ThrowKey = Animator.StringToHash("throw");
        private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");

        private bool allowDoubleJump;
        private bool isOnWall;
        private bool superThrow;

        private GameSession session;
        private HealthComponent health;
        private CameraShakeEffect cameraShake;
        private float defaultGravityScale;

        private const string SwordId = "Sword";
        private int CoinsCount => session.Data.Inventory.Count("Coin");
        private int SwordCount => session.Data.Inventory.Count(SwordId);

        private string SelectedItemId => session.QuickInventory.SelectedItem.Id;

        private bool CanThrow
        {
            get
            {
                if (SelectedItemId == SwordId)
                    return SwordCount > 1;

                ItemConfig def = CoreGameConfigs.ConfigsInstance.Items.Get(SelectedItemId);
                return def.HasTag(ItemTag.Throwable);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            defaultGravityScale = rigidbody.gravityScale;
        }

        //Refactoring
        private void Start()
        {
            cameraShake = FindObjectOfType<CameraShakeEffect>();
            session = FindObjectOfType<GameSession>();
            health = GetComponent<HealthComponent>();
            session.Data.Inventory.OnChanged += OnInventoryChanged;
            session.StatsModel.OnUpgraded += OnHeroUpgraded;

            health.SetHealth(session.Data.Hp.Value);
            UpdateHeroWeapon();
        }

        private void OnHeroUpgraded(StatId statId)
        {
            switch (statId)
            {
                case StatId.Hp:
                    var health = (int) session.StatsModel.GetValue(statId);
                    session.Data.Hp.Value = health;
                    this.health.SetHealth(health);
                    break;
            }
        }

        private void OnDestroy() =>
            session.Data.Inventory.OnChanged -= OnInventoryChanged;

        private void OnInventoryChanged(string id, int value)
        {
            if (id == SwordId)
                UpdateHeroWeapon();
        }

        public void OnHealthChanged(int currentHealth) =>
            session.Data.Hp.Value = currentHealth;

        protected override void Update()
        {
            base.Update();

            var moveToSameDirection = direction.x * transform.lossyScale.x > 0;
            if (wallCheck.IsTouchingLayer && moveToSameDirection)
            {
                isOnWall = true;
                rigidbody.gravityScale = 0;
            }
            else
            {
                isOnWall = false;
                rigidbody.gravityScale = defaultGravityScale;
            }

            animator.SetBool(IsOnWall, isOnWall);
        }

        protected override float CalculateYVelocity()
        {
            var isJumpPressing = direction.y > 0;

            if (isGrounded || isOnWall)
            {
                allowDoubleJump = true;
            }

            if (!isJumpPressing && isOnWall)
            {
                return 0f;
            }

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if (!isGrounded && allowDoubleJump && session.PerksModel.IsDoubleJumpSupported && !isOnWall)
            {
                session.PerksModel.Cooldown.Reset();
                allowDoubleJump = false;
                DoJumpVfx();
                return jumpSpeed;
            }

            return base.CalculateJumpVelocity(yVelocity);
        }

        public void AddInInventory(string id, int value)
        {
            session.Data.Inventory.Add(id, value);
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            cameraShake?.Shake();
            if (CoinsCount > 0)
            {
                SpawnCoins();
            }
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(CoinsCount, 5);
            session.Data.Inventory.Remove("Coin", numCoinsToDispose);

            hitDrop.SetCount(numCoinsToDispose);
            hitDrop.CalculateDrop();
        }

        public void Interact()
        {
            interactionCheck.Check();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(groundLayer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= slamDownVelocity)
                {
                    particles.Spawn("SlamDown");
                }
            }
        }

        public override void Attack()
        {
            if (SwordCount <= 0) return;

            base.Attack();
        }

        private void UpdateHeroWeapon() =>
            animator.runtimeAnimatorController = SwordCount > 0 ? armed : disarmed;

        public void OnDoThrow()
        {
            if (superThrow && session.PerksModel.IsSuperThrowSupported)
            {
                var throwableCount = session.Data.Inventory.Count(SelectedItemId);
                var possibleCount = SelectedItemId == SwordId ? throwableCount - 1 : throwableCount;

                var numThrows = Mathf.Min(superThrowParticles, possibleCount);
                session.PerksModel.Cooldown.Reset();
                StartCoroutine(DoSuperThrow(numThrows));
            }
            else
            {
                ThrowAndRemoveFromInventory();
            }

            superThrow = false;
        }

        private IEnumerator DoSuperThrow(int numThrows)
        {
            for (int i = 0; i < numThrows; i++)
            {
                ThrowAndRemoveFromInventory();
                yield return new WaitForSeconds(superThrowDelay);
            }
        }

        private void ThrowAndRemoveFromInventory()
        {
            sounds.Play("Range");

            var throwableId = session.QuickInventory.SelectedItem.Id;
            var throwableDef = CoreGameConfigs.ConfigsInstance.Throwable.Get(throwableId);
            throwSpawner.SetPrefab(throwableDef.Projectile);
            var instance = throwSpawner.SpawnInstance();
            ApplyRangeDamageStat(instance);

            session.Data.Inventory.Remove(throwableId, 1);
        }

        private void ApplyRangeDamageStat(GameObject projectile)
        {
            var hpModify = projectile.GetComponent<ModifyHealthComponent>();
            var damageValue = (int) session.StatsModel.GetValue(StatId.RangeDamage);
            damageValue = ModifyDamageByCrit(damageValue);
            hpModify.SetDelta(-damageValue);
        }

        private int ModifyDamageByCrit(int damage)
        {
            var critChange = session.StatsModel.GetValue(StatId.CriticalDamage);
            if (Random.value * 100 <= critChange)
            {
                return damage * 2;
            }

            return damage;
        }

        public void StartThrowing()
        {
            superThrowCooldown.Reset();
        }

        public void UseInventory()
        {
            if (IsSelectedItem(ItemTag.Throwable))
                PerformThrowing();
            else if (IsSelectedItem(ItemTag.Potion))
                UsePotion();
        }

        private void UsePotion()
        {
            var potion = CoreGameConfigs.ConfigsInstance.Potions.Get(SelectedItemId);

            switch (potion.Effect)
            {
                case Effect.AddHp:
                    session.Data.Hp.Value += (int) potion.Value;
                    break;
                case Effect.SpeedUp:
                    _speedUpCooldown.Value = _speedUpCooldown.RemainingTime + potion.Time;
                    _additionalSpeed = Mathf.Max(potion.Value, _additionalSpeed);
                    _speedUpCooldown.Reset();
                    break;
            }

            session.Data.Inventory.Remove(potion.Id, 1);
        }

        private readonly Cooldown _speedUpCooldown = new Cooldown();
        private float _additionalSpeed;

        protected override float CalculateSpeed()
        {
            if (_speedUpCooldown.IsReady)
                _additionalSpeed = 0f;

            var defaultSpeed = session.StatsModel.GetValue(StatId.Speed);
            return defaultSpeed + _additionalSpeed;
        }

        private bool IsSelectedItem(ItemTag tag)
        {
            return session.QuickInventory.SelectedDef.HasTag(tag);
        }

        private void PerformThrowing()
        {
            if (!throwCooldown.IsReady || !CanThrow) return;

            if (superThrowCooldown.IsReady) superThrow = true;

            animator.SetTrigger(ThrowKey);
            throwCooldown.Reset();
        }

        public void NextItem()
        {
            session.QuickInventory.SetNextItem();
        }

        public void DropDown()
        {
            var endPosition = transform.position + new Vector3(0, -1);
            var hit = Physics2D.Linecast(transform.position, endPosition, groundLayer);
            if (hit.collider == null) return;
            var component = hit.collider.GetComponent<TmpDisableColliderComponent>();
            if (component == null) return;
            component.DisableCollider();
        }

        public void UsePerk()
        {
            if (session.PerksModel.IsShieldSupported)
            {
                shield.Use();
                session.PerksModel.Cooldown.Reset();
            }
        }

        public void ToggleFlashlight()
        {
            var isActive = flashlight.gameObject.activeSelf;
            flashlight.gameObject.SetActive(!isActive);
        }
    }
}