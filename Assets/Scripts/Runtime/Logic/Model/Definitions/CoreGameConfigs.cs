using UnityEngine;
using Scripts.Model.Definitions.Player;
using Scripts.Model.Definitions.Repositories;
using Scripts.Model.Definitions.Repositories.Items;

namespace Scripts.Model.Definitions
{
    [CreateAssetMenu(menuName = "Platformer 2D/ Core / Repository / Core Game Config", fileName = "Core-game-configs-so")]
    public class CoreGameConfigs : ScriptableObject, ICoreGameConfigs
    {
        [SerializeField]
        private ItemsRepository items;
        [SerializeField]
        private ThrowableRepository throwableItems;
        [SerializeField]
        private PotionRepository potions;
        [SerializeField]
        private PerkRepository perks;
        [SerializeField]
        private PlayerConfigs player;

        public ItemsRepository Items => items;
        public ThrowableRepository Throwable => throwableItems;
        public PotionRepository Potions => potions;
        public PerkRepository Perks => perks;
        public PlayerConfigs Player => player;


        //TODO Remove
        private static CoreGameConfigs instance;
        public static CoreGameConfigs ConfigsInstance =>  instance;
    }
}