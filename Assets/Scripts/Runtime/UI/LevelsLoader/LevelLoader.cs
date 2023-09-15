using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Scripts.UI.LevelsLoader
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float transitionTime;

        private static readonly int Enabled = Animator.StringToHash("Enabled");
        private AsyncOperation asyncOperation;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void OnAfterSceneLoad()
        {
            InitLoader();
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        
        private static void InitLoader()
        {
            SceneManager.LoadScene("LevelLoader", LoadSceneMode.Additive);
        }

        public void LoadLevel(string sceneName)
        {
            StartCoroutine(StartAnimation(sceneName));
        }

        private IEnumerator StartAnimation(string sceneName)
        {
            animator.SetBool(Enabled, true);
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(sceneName);
            animator.SetBool(Enabled, false);
        }
    }
}