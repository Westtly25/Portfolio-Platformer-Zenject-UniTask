using Scripts.Utilities;
using UnityEngine;

namespace Scripts.Components
{
    public class ShowWindowComponent : MonoBehaviour
    {
        [SerializeField] private string path;

        public void Show()
        {
            WindowUtils.CreateWindow(path);
        }
    }
}