using System;
using UnityEngine;

namespace Scripts.Model.Data
{
    [Serializable]
    public struct DialogData
    {
        [SerializeField]
        private Sentence[] sentences;

        [SerializeField]
        private DialogType type;
        public Sentence[] Sentences => sentences;
        public DialogType Type => type;
    }
}