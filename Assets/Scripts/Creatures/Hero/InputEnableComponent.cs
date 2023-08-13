using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Creatures.Hero
{
    public class InputEnableComponent : MonoBehaviour
    {
        private PlayerInput input;

        private void Start() =>
            input = FindObjectOfType<PlayerInput>();

        public void SetInput(bool isEnabled) =>
            input.enabled = isEnabled;
    }
}