using Scripts.Model.Data;
using UnityEngine;

namespace Scripts.UI.Hud.Dialogs
{
    public class PersonalizedDialogBoxController : DialogBoxController
    {
        [SerializeField] private DialogContent right;

        protected override DialogContent CurrentContent =>
            CurrentSentence.Side == Side.Left ? content : right;

        protected override void OnStartDialogAnimation()
        {
            right.gameObject.SetActive(CurrentSentence.Side == Side.Right);
            content.gameObject.SetActive(CurrentSentence.Side == Side.Left);

            base.OnStartDialogAnimation();
        }
    }
}