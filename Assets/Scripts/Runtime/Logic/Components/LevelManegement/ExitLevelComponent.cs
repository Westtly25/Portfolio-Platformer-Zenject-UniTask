using UnityEngine;
using Scripts.Model;
using Scripts.UI.LevelsLoader;

namespace Scripts.Components.LevelManegement
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField]
        private string sceneName;

        public void Exit()
        {
            var session = FindObjectOfType<GameSession>();
            session.Save();
            var loader = FindObjectOfType<LevelLoader>();
            //TODO
            //loader.LoadLevel(sceneName);
        }
    }
}