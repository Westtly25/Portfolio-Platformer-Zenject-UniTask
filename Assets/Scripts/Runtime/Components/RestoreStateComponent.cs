using Scripts.Model;
using UnityEngine;

namespace Scripts.Components
{
    public class RestoreStateComponent : MonoBehaviour
    {
        [SerializeField] private string id;
        public string Id => id;

        private GameSession session;

        private void Start()
        {
            session = FindObjectOfType<GameSession>();
            var isDestroyed = session.RestoreState(Id);

            if (isDestroyed)
                Destroy(gameObject);
        }
    }
}