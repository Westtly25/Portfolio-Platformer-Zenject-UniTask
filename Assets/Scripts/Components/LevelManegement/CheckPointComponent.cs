using Scripts.Components.GoBased;
using Scripts.Model;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Scripts.Components.LevelManegement
{
    [RequireComponent(typeof(SpawnComponent))]
    public class CheckPointComponent : MonoBehaviour
    {
        [SerializeField] private string id;
        [SerializeField] private SpawnComponent heroSpawner;
        [SerializeField] private UnityEvent setChecked;
        [SerializeField] private UnityEvent setUnchecked;

        public string Id => id;
        private GameSession session;

        [Inject]
        public void Constructor(GameSession session)
        {
            this.session = session;
        }

        private void Start()
        {
            if (session.IsChecked(id))
                setChecked?.Invoke();
            else
                setUnchecked?.Invoke();
        }

        public void Check()
        {
            session.SetChecked(id);
            setChecked?.Invoke();
        }

        public void SpawnHero()
        {
            heroSpawner.Spawn();
        }
    }
}