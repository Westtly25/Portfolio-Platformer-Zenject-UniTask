using System;
using Scripts.Model.Definitions.Repositories.Items;
using UnityEngine;

namespace Scripts.Model.Definitions.Repositories
{
    [CreateAssetMenu(menuName = "Defs/Repository/Throwable", fileName = "Throwable")]
    public class ThrowableRepository : DefRepository<ThrowableDef>
    {
    }

    [Serializable]
    public struct ThrowableDef : IHaveId
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private GameObject _projectile;

        public string Id => _id;
        public GameObject Projectile => _projectile;
    }
}