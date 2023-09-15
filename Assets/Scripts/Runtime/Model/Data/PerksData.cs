using System;
using System.Collections.Generic;
using Scripts.Model.Data.Properties;
using UnityEngine;

namespace Scripts.Model.Data
{
    [Serializable]
    public class PerksData
    {
        [SerializeField]
        private StringProperty used = new();
        [SerializeField]
        private List<string> unlocked;
        public StringProperty Used => used;

        public void AddPerk(string id)
        {
            if (!unlocked.Contains(id))
                unlocked.Add(id);
        }

        public bool IsUnlocked(string id) =>
             unlocked.Contains(id);
    }
}