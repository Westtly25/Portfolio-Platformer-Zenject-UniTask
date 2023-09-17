using System;
using UnityEngine;

namespace Scripts.Model.Definitions.Repositories
{
    [Serializable]
    public struct PerkConfig : IHaveId
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
}