using UnityEngine;
using Cinemachine;
using System.Collections;

namespace Scripts.Effects.CameraRelated
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraShakeEffect : MonoBehaviour
    {
        [SerializeField] private float animationTime = 0.3f;
        [SerializeField] private float intensity = 3f;

        private CinemachineBasicMultiChannelPerlin cameraNoise;

        private Coroutine coroutine;

        private void Awake()
        {
            var virtualCamera = GetComponent<CinemachineVirtualCamera>();
            cameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void Shake()
        {
            if (coroutine != null)
                StopAnimation();
            coroutine = StartCoroutine(StartAnimation());
        }

        private IEnumerator StartAnimation()
        {
            cameraNoise.m_FrequencyGain = intensity;
            yield return new WaitForSeconds(animationTime);
            StopAnimation();
        }


        private void StopAnimation()
        {
            cameraNoise.m_FrequencyGain = 0f;
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}