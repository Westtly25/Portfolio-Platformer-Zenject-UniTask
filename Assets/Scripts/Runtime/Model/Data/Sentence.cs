using System;
using UnityEngine;

namespace Scripts.Model.Data
{
    [Serializable]
    public struct Sentence
    {
        [SerializeField]
        private string valued;
        [SerializeField]
        private Sprite icon;
        [SerializeField]
        private Side side;

        public string Value => valued;
        public Sprite Icon => icon;
        public Side Side => side;
    }
}