using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Scripts.Components
{
    public class TimerComponent : MonoBehaviour
    {
        [SerializeField]
        private TimerData[] timers;

        public void Set(int index)
        {
            TimerData timer = timers[index];

            if (timer.Coroutine != null)
                StopCoroutine(timer.Coroutine);

            timer.Coroutine = StartCoroutine(StartTimer(timer));
        }

        private IEnumerator StartTimer(TimerData timerData)
        {
            yield return new WaitForSeconds(timerData.Delay);
            timerData.TimesUp?.Invoke();
        }
    }

    [Serializable]
    public class TimerData
    {
        public float Delay;
        public UnityEvent TimesUp;
        public Coroutine Coroutine;
    }
}