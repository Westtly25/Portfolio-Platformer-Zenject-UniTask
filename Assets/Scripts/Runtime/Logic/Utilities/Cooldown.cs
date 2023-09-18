using System;
using UnityEngine;

namespace Scripts.Utilities
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField]
        private float value;

        [SerializeField]
        private float timesUp;

        public float Value
        {
            get => value;
            set => this.value = value;
        }

        public void Reset() =>
            timesUp = Time.time + value;

        public float RemainingTime =>
            Mathf.Max(timesUp - Time.time, 0);

        public bool IsReady =>
            timesUp <= Time.time;
    }
}