using System;
using UnityEngine;

namespace Scripts.Components.Audio
{
    public partial class PlaySoundsComponent
    {
        [Serializable]
        public class AudioData
        {
            [SerializeField]
            private string id;

            [SerializeField]
            private AudioClip clip;

            public string Id => id;
            public AudioClip Clip => clip;
        }
    }
}