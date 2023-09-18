using UnityEngine;
using Scripts.Utilities;
using Scripts.Utilities.ObjectPool;

namespace Scripts.Components.GoBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform target;
        [SerializeField]
        private GameObject prefab;
        [SerializeField]
        private bool usePool;

        [ContextMenu("Spawn")]
        public void Spawn() =>
            SpawnInstance();

        public GameObject SpawnInstance()
        {
            var targetPosition = target.position;

            var instance = usePool
                ? Pool.Instance.Get(prefab, targetPosition)
                : SpawnUtils.Spawn(prefab, targetPosition);

            var scale = target.lossyScale;
            instance.transform.localScale = scale;
            instance.SetActive(true);
            return instance;
        }

        public void SetPrefab(GameObject prefab) =>
            this.prefab = prefab;
    }
}