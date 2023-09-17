using System;
using Scripts.Model.Data;
using Scripts.Model.Data.Properties;
using UnityEngine;

namespace Scripts.Components.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSettingComponent : MonoBehaviour
    {
        [SerializeField] private SoundSetting mode;
        private AudioSource source;
        private FloatPersistentProperty model;

        private void Start()
        {
            source = GetComponent<AudioSource>();

            model = FindProperty();
            model.OnChanged += OnSoundSettingChanged;
            OnSoundSettingChanged(model.Value, model.Value);
        }

        private void OnSoundSettingChanged(float newValue, float oldValue)
        {
            source.volume = newValue;
        }

        private FloatPersistentProperty FindProperty()
        {
            switch (mode)
            {
                case SoundSetting.Music:
                    return GameSettings.I.Music;
                case SoundSetting.Sfx:
                    return GameSettings.I.Sfx;
            }

            throw new ArgumentException("Undefined mode");
        }

        private void OnDestroy()
        {
            model.OnChanged -= OnSoundSettingChanged;
        }
    }
}