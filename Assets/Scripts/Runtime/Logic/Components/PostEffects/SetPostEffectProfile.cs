using UnityEngine;
using UnityEngine.Rendering;

namespace Scripts.Components.PostEffects
{
    public class SetPostEffectProfile : MonoBehaviour
    {
        [SerializeField]
        private VolumeProfile profile;

        public void Set()
        {
            Volume[] volumes = FindObjectsOfType<Volume>();

            foreach (var volume in volumes)
            {
                if (!volume.isGlobal)
                    continue;

                volume.profile = profile;
                break;
            }
        }
    }
}