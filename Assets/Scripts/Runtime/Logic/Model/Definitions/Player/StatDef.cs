using System;
using UnityEngine;
using Scripts.Model.Definitions.Repositories;

namespace Scripts.Model.Definitions.Player
{
    [Serializable]
    public struct StatDef
    {
        [SerializeField] private string name;
        [SerializeField] private StatId id;
        [SerializeField] private Sprite icon;
        [SerializeField] private StatLevelDef[] levels;

        public StatId ID => id;
        public string Name => name;
        public Sprite Icon => icon;
        public StatLevelDef[] Levels => levels;
    }

    [Serializable]
    public struct StatLevelDef
    {
        [SerializeField] private float value;
        [SerializeField] private ItemWithCount price;

        public float Value => value;

        public ItemWithCount Price => price;
    }

    public enum StatId
    {
        Hp,
        Speed,
        RangeDamage,
        CriticalDamage
    }
}