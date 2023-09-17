using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Components
{
    public class ProbabilityDropComponent : MonoBehaviour
    {
        [SerializeField] private int count;
        [SerializeField] private DropData[] drop;
        [SerializeField] private DropEvent onDropCalculated;
        [SerializeField] private bool spawnOnEnable;

        private void OnEnable()
        {
            if (spawnOnEnable)
                CalculateDrop();
        }

        [ContextMenu("CalculateDrop")]
        public void CalculateDrop()
        {
            var itemsToDrop = new GameObject[count];
            var itemCount = 0;
            var total = drop.Sum(dropData => dropData.Probability);
            var sortedDrop = drop.OrderBy(dropData => dropData.Probability).ToArray();

            while (itemCount < count)
            {
                var random = UnityEngine.Random.value * total;
                var current = 0f;
                foreach (var dropData in sortedDrop)
                {
                    current += dropData.Probability;
                    if (current >= random)
                    {
                        itemsToDrop[itemCount] = dropData.Drop;
                        itemCount++;
                        break;
                    }
                }
            }

            onDropCalculated?.Invoke(itemsToDrop);
        }


        [Serializable]
        public class DropData
        {
            [SerializeField]
            public GameObject Drop;

            [SerializeField]
            [Range(0f, 100f)]
            public float Probability;
        }

        public void SetCount(int count) =>
            this.count = count;
    }

    [Serializable]
    public class DropEvent : UnityEvent<GameObject[]>
    {
    }
}