using System;
using Scripts.Model.Data;
using Scripts.Model.Definitions;
using Scripts.UI.Hud.Dialogs;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Components.Dialogs
{
    public class ShowDialogComponent : MonoBehaviour
    {
        [SerializeField]
        private Mode mode;
        [SerializeField]
        private DialogData bound;
        [SerializeField]
        private DialogDef external;
        [SerializeField]
        private UnityEvent onComplete;

        private DialogBoxController _dialogBox;

        public void Show()
        {
            _dialogBox = DialogController();
            _dialogBox.ShowDialog(Data, onComplete);
        }

        private DialogBoxController DialogController()
        {
            if (_dialogBox != null) return _dialogBox;

            GameObject controllerGo;
            switch (Data.Type)
            {
                case DialogType.Simple:
                    controllerGo = GameObject.FindWithTag("SimpleDialog");
                    break;
                case DialogType.Personalized:
                    controllerGo = GameObject.FindWithTag("PersonalizedDialog");
                    break;
                default:
                    throw new ArgumentException("Undefined dialog type");
            }

            return controllerGo.GetComponent<DialogBoxController>();
        }

        public void Show(DialogDef def)
        {
            external = def;
            Show();
        }

        public DialogData Data
        {
            get
            {
                switch (mode)
                {
                    case Mode.Bound:
                        return bound;
                    case Mode.External:
                        return external.Data;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public enum Mode
        {
            Bound,
            External
        }
    }
}