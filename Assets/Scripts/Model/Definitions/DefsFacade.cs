using Scripts.Model.Definitions.Player;
using Scripts.Model.Definitions.Repositories;
using Scripts.Model.Definitions.Repositories.Items;
using UnityEngine;

namespace Scripts.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]
    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private ItemsRepository items;
        [SerializeField] private ThrowableRepository throwableItems;
        [SerializeField] private PotionRepository potions;
        [SerializeField] private PerkRepository perks;
        [SerializeField] private PlayerDef player;

        public ItemsRepository Items => items;
        public ThrowableRepository Throwable => throwableItems;
        public PotionRepository Potions => potions;
        public PerkRepository Perks => perks;
        public PlayerDef Player => player;

        private static DefsFacade _instance;
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade LoadDefs()
        {
            return _instance = Resources.Load<DefsFacade>("DefsFacade");
        }
    }
}