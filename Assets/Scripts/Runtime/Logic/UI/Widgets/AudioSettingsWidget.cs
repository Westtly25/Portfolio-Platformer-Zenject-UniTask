using Scripts.Model.Data.Properties;
using Scripts.Utilities.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Widgets
{
    public class AudioSettingsWidget : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private Text value;

        private FloatPersistentProperty model;

        private readonly CompositeDisposable trash = new();

        private void Start() =>
            trash.Retain(slider.onValueChanged.Subscribe(OnSliderValueChanged));

        public void SetModel(FloatPersistentProperty model)
        {
            this.model = model;
            trash.Retain(model.Subscribe(OnValueChanged));
            OnValueChanged(model.Value, model.Value);
        }

        private void OnSliderValueChanged(float value)
        {
            model.Value = value;
        }

        private void OnValueChanged(float newValue, float oldValue)
        {
            var textValue = Mathf.Round(newValue * 100);
            value.text = textValue.ToString();

            slider.normalizedValue = newValue;
        }

        private void OnDestroy()
        {
            trash.Dispose();
        }
    }
}