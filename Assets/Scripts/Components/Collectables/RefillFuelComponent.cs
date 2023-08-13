using Zenject;
using UnityEngine;
using Scripts.Model;

namespace Scripts.Components.Collectables
{
    public class RefillFuelComponent : MonoBehaviour
    {
        private GameSession session;

        [Inject]
        public void Constructor(GameSession gameSession) =>
            this.session = gameSession;

        public void Refill() =>
            session.Data.Fuel.Value = 100;
    }
}