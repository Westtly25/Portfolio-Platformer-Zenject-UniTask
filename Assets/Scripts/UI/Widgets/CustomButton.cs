using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Widgets
{
    public class CustomButton : Button
    {
        [SerializeField] private GameObject normal;
        [SerializeField] private GameObject pressed;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);

            var isPressed = state == SelectionState.Pressed || state == SelectionState.Disabled;
            normal.SetActive(!isPressed);
            pressed.SetActive(isPressed);
        }
    }
}