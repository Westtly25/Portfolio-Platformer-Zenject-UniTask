using Cinemachine;
using UnityEngine;
using Scripts.Creatures.Hero;

namespace Scripts.Components.LevelManegement
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class SetFollowComponent : MonoBehaviour
    {
        private void Start()
        {
            CinemachineVirtualCamera vCamera = GetComponent<CinemachineVirtualCamera>();
            vCamera.Follow = FindObjectOfType<Hero>().transform;
        }
    }
}