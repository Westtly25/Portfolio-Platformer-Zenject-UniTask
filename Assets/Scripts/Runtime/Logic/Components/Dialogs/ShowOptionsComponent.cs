using Scripts.UI.Hud.Dialogs;
using UnityEngine;

namespace Scripts.Components.Dialogs
{
    public class ShowOptionsComponent : MonoBehaviour
    {
        [SerializeField] private OptionDialogData data;
        private OptionDialogController dialogBox;

        public void Show()
        {
            if (dialogBox == null)
                dialogBox = FindObjectOfType<OptionDialogController>();

            dialogBox.Show(data);
        }
    }
}