using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Model.Definitions.Repositories
{
    public class DefRepository<TDefType> : ScriptableObject where TDefType : IHaveId
    {
        [SerializeField] protected TDefType[] collection;

        public TDefType Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return default;

            for (int i = 0; i < collection.Length; i++)
            {
                TDefType itemDef = collection[i];
                if (itemDef.Id == id)
                    return itemDef;
            }

            return default;
        }

        public TDefType[] All => new List<TDefType>(collection).ToArray();
    }
}