using System;
using UnityEngine;
using Scripts.Model.Definitions.Repositories.Items;

namespace Scripts.Model.Definitions.Repositories
{
    [CreateAssetMenu(menuName = "Platformer 2D/ Core / Repository / Potions", fileName = "Potions-repository-so")]
    public class PotionRepository : ConfigsRepository<PotionDef>
    {
    }

    [Serializable]
    public struct PotionDef : IHaveId
    {
        [InventoryId, SerializeField]
        private string id;
        [SerializeField]
        private Effect effect;
        [SerializeField]
        private float value;
        [SerializeField]
        private float time;
        public string Id => id;
        public Effect Effect => effect;
        public float Value => value;
        public float Time => time;
    }

    public enum Effect
    {
        AddHp,
        SpeedUp
    }
}