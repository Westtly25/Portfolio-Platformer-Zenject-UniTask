using System;
using Scripts.Model.Definitions.Repositories.Items;
using UnityEngine;

namespace Scripts.Model.Definitions.Repositories
{
    [CreateAssetMenu(menuName = "Platformer 2D/ Core / Repository / Perks", fileName = "Perks-repository-so")]
    public class PerkRepository : ConfigsRepository<PerkDef>
    {
    }

    [Serializable]
    public struct PerkDef : IHaveId
    {
        [SerializeField] private string id;
        [SerializeField] private Sprite icon;
        [SerializeField] private string info;
        [SerializeField] private ItemWithCount price;
        [SerializeField] private float _cooldown;

        public string Id => id;
        public Sprite Icon => icon;
        public string Info => info;
        public ItemWithCount Price => price;
        public float Cooldown => _cooldown;
    }

    [Serializable]
    public struct ItemWithCount
    {
        [InventoryId, SerializeField]
        private string itemId;
        [SerializeField]
        private int count;

        public string ItemId => itemId;
        public int Count => count;
    }
}