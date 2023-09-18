using Scripts.Model.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Architecture.Services.Save_Service
{
    [Serializable]
    public sealed class GameData
    {
        [SerializeField]
        [Range(byte.MinValue, byte.MaxValue)]
        public int ID;

        [SerializeField]
        public List<InventoryItemData> Inventory;

    }
}