using Scripts.Model;
using UnityEngine;


namespace Scripts.Creatures.Hero.Features
{
    public class HeroFlashlight : MonoBehaviour
    {
        [SerializeField] private float consumePerSecond;
        [SerializeField] private UnityEngine.Rendering.Universal.Light2D light;

        private GameSession session;
        private float defaultIntensity;

        private void Start()
        {
            session = FindObjectOfType<GameSession>();
            defaultIntensity = light.intensity;
        }

        private void Update()
        {
            var consumed = Time.deltaTime * consumePerSecond;
            var currentValue = session.Data.Fuel.Value;
            var nextValue = currentValue - consumed;
            nextValue = Mathf.Max(nextValue, 0);
            session.Data.Fuel.Value = nextValue;

            var progress = Mathf.Clamp(nextValue / 20, 0, 1);
            light.intensity = defaultIntensity * progress;
        }
    }
}