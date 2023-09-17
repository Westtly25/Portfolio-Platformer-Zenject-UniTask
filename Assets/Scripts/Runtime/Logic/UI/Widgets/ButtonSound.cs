using Scripts.Components.Audio;
using Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.UI.Widgets
{
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip _audioClip;

        private AudioSource source;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (source == null)
                source = AudioUtils.FindSfxSource();

            source.PlayOneShot(_audioClip);
        }
    }
}