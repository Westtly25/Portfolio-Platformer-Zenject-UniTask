using System;
using Scripts.UI.Widgets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.UI.Hud.Dialogs
{
    public class OptionDialogController : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        [SerializeField] private Text contentText;
        [SerializeField] private Transform optionsContainer;
        [SerializeField] private OptionItemWidget prefab;

        private DataGroup<OptionData, OptionItemWidget> dataGroup;

        private void Start()
        {
            dataGroup = new DataGroup<OptionData, OptionItemWidget>(prefab, optionsContainer);
        }

        public void OnOptionsSelected(OptionData selectedOption)
        {
            selectedOption.OnSelect.Invoke();
            content.SetActive(false);
        }

        public void Show(OptionDialogData data)
        {
            content.SetActive(true);
            contentText.text = data.DialogText;

            dataGroup.SetData(data.Options);
        }
    }

    [Serializable]
    public class OptionDialogData
    {
        public string DialogText;
        public OptionData[] Options;
    }

    [Serializable]
    public class OptionData
    {
        public string Text;
        public UnityEvent OnSelect;
    }
}