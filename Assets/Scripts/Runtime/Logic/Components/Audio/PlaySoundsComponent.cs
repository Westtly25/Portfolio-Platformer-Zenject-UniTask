using Scripts.Utilities;
using UnityEngine;

namespace Scripts.Components.Audio
{
    public partial class PlaySoundsComponent : MonoBehaviour
    {
        public const string SfxSourceTag = "SfxAudioSource";

        [SerializeField]
        private AudioData[] sounds;

        private AudioSource source;

        public void Play(string id)
        {
            foreach (var audioData in sounds)
            {
                if (audioData.Id != id) continue;

                if (source == null)
                    source = AudioUtils.FindSfxSource();

                source.PlayOneShot(audioData.Clip);
                break;
            }
        }
    }
}