using UnityEngine;

namespace Scripts.Model.Definitions.Repositories.Items
{
    [CreateAssetMenu(menuName = "Platformer 2D/ Core / Repository / Items", fileName = "Items-repository-so")]
    public class ItemsRepository : ConfigsRepository<ItemConfig>
    {
#if UNITY_EDITOR
        public ItemConfig[] ItemsForEditor => collection;
#endif
    }
}