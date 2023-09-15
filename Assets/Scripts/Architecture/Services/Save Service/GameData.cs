using System;
using UnityEngine;

namespace Assets.Scripts.Architecture.Services.Save_Service
{
    [Serializable]
    public sealed class GameData
    {
        [SerializeField]
        [Range(byte.MinValue, byte.MaxValue)]
        public int ID;

    }
}