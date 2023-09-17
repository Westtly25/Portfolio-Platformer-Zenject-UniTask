using System;
using Scripts.Model.Definitions.Repositories.Items;
using UnityEngine;

namespace Scripts.Model.Definitions.Repositories
{
    [CreateAssetMenu(menuName = "Platformer 2D/ Core / Repository / Throwable", fileName = "Throwable-repository-so")]
    public class ThrowableRepository : ConfigsRepository<ThrowableDef>
    {
    }

    [Serializable]
    public struct ThrowableDef : IHaveId
    {
        [InventoryId, SerializeField]
        private string id;
        [SerializeField]
        private GameObject projectile;

        public string Id => id;
        public GameObject Projectile => projectile;
    }
}