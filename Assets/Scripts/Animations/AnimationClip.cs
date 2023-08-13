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
        [SerializeField] private bool islooped;
        [SerializeField] private bool allowNextClip;
        [SerializeField] private UnityEvent onComplete;

        public string Name => name;
        public Sprite[] Sprites => sprites;
        public bool Loop => islooped;
        public bool AllowNextClip => allowNextClip;
        public UnityEvent OnComplete => onComplete;
    }
}