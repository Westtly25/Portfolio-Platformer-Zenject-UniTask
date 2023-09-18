using Scripts.Utilities;
using UnityEngine;

namespace Scripts.Components.Audio
{
    public class PlaySfxSound : MonoBehaviour
    {
        [SerializeField]
        private AudioClip clip;

        private AudioSource source;

        public void Play()
        {
            if (source == null)
                source = AudioUtils.FindSfxSource();

            source.PlayOneShot(clip);
        }
    }
}