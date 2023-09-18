using System.Collections.Generic;

namespace Scripts.Utilities
{
    public class Lock
    {
        private readonly List<object> retained = new();

        public void Retain(object item) =>
            retained.Add(item);

        public void Release(object item) =>
            retained.Remove(item);

        public bool IsLocked =>
            retained.Count > 0;
    }
}