using System;
using UnityEngine;

namespace Scripts.Model.Data
{
    [Serializable]
    public struct DialogData
    {
        [SerializeField] private Sentence[] sentences;
        [SerializeField] private DialogType type;
        public Sentence[] Sentences => sentences;
        public DialogType Type => type;
    }

    [Serializable]
    public struct Sentence
    {
        [SerializeField] private string valued;
        [SerializeField] private Sprite icon;
        [SerializeField] private Side side;

        public string Value => valued;
        public Sprite Icon => icon;
        public Side Side => side;
    }

    public enum Side
    {
        Left,
        Right
    }

    public enum DialogType
    {
        Simple,
        Personalized
    }
}