using UnityEngine;

namespace Scripts.Model.Definitions.Player
{
    [CreateAssetMenu(menuName = "Platformer 2D/ Core / Configs / PlayerConfigs", fileName = "Player-configs-so")]
    public class PlayerConfigs : ScriptableObject
    {
        [SerializeField]
        private int inventorySize;
        [SerializeField]
        private StatDef[] stats;

        public int InventorySize => inventorySize;
        public StatDef[] Stats => stats;

        public StatDef GetStat(StatId id)
        {
            foreach (var statDef in stats)
            {
                if (statDef.ID == id)
                {
                    return statDef;
                }
            }

            return default;
        }
    }
}