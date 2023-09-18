using Scripts.Model;
using Scripts.Model.Definitions.Repositories;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Hud
{
    public class CurrentPerkWidget : MonoBehaviour
    {
        [SerializeField]
        private Image icon;
        [SerializeField]
        private Image cooldownImage;
        
        //TODO
        private GameSession session;

        private void Start()
        {
            session = FindObjectOfType<GameSession>();
        }

        public void Initialize()
        {

        }

        public void Set(PerkConfig perkDef)
        {
            icon.sprite = perkDef.Icon;
        }

        private void Update()
        {
            Utilities.Cooldown cooldown = session.PerksModel.Cooldown;
            cooldownImage.fillAmount = cooldown.RemainingTime / cooldown.Value;
        }
    }
}