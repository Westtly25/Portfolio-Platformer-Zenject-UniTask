using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Utilities.ObjectPool
{
    public class PoolItem : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onRestart;

        private int id;
        private Pool pool;

        public void Restart()
        {
            onRestart?.Invoke();
        }

        public void Release()
        {
            pool.Release(id, this);
        }

        public void Retain(int id, Pool pool)
        {
            this.id = id;
            this.pool = pool;
        }
    }
}