using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Animations
{
    [Serializable]
    public class AnimationClip
    {
        [SerializeField]
        private string name;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private bool isLooped;
        [SerializeField] private bool allowNextClip;
        [SerializeField] private UnityEvent onComplete;

        public string Name => name;
        public Sprite[] Sprites => sprites;
        public bool Loop => isLooped;
        public bool AllowNextClip => allowNextClip;
        public UnityEvent OnComplete => onComplete;
    }
}