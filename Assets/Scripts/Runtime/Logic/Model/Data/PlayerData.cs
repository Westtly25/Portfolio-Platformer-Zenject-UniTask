using System;
using UnityEngine;
using Scripts.Model.Data.Properties;

namespace Scripts.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField]
        private InventoryHandler inventory;

        public IntProperty Hp = new();
        public FloatProperty Fuel = new();
        public PerksData Perks = new();
        public LevelData Levels = new();
        public InventoryHandler Inventory => inventory;

        public PlayerData Clone()
        {
            string json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}