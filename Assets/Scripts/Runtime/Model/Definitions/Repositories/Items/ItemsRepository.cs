using System;
using System.Linq;
using UnityEngine;

namespace Scripts.Model.Definitions.Repositories.Items
{
    [CreateAssetMenu(menuName = "Defs/Repository/Items", fileName = "Items")]
    public class ItemsRepository : DefRepository<ItemDef>
    {
#if UNITY_EDITOR
        public ItemDef[] ItemsForEditor => collection;
#endif
    }

    [Serializable]
    public struct ItemDef : IHaveId
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private Sprite icon;
        [SerializeField]
        private ItemTag[] tags;

        public string Id => id;
        public bool IsVoid => string.IsNullOrEmpty(id);
        public Sprite Icon => icon;

        public bool HasTag(ItemTag tag) =>
            tags?.Contains(tag) ?? false;
    }
}