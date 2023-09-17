using Scripts.Model;
using UnityEngine;

namespace Scripts.Components.GoBased
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject objectToDestroy;
        [SerializeField] private RestoreStateComponent state;

        public void DestroyObject()
        {
            Destroy(objectToDestroy);

            if (state != null)
                FindObjectOfType<GameSession>().StoreState(state.Id);
        }
    }
}