using System.Linq;
using UnityEngine;

namespace Scripts.Components.GoBased
{
    public partial class SpawnListComponent : MonoBehaviour
    {
        [SerializeField]
        private SpawnData[] spawners;

        public void SpawnAll()
        {
            foreach (var spawnData in spawners)
            {
                spawnData.Component.Spawn();
            }
        }

        public void Spawn(string id)
        {
            var spawner = spawners.FirstOrDefault(element => element.Id == id);
            spawner?.Component.Spawn();
        }
    }
}