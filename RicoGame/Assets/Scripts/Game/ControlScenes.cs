using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game{
    public class ControlScenes : MonoBehaviour
    {
    
        void Awake()
        {
            if (FindObjectsOfType<ControlScenes>().Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        // Start is called before the first frame update
        public void RestartGame()
        {
            Time.timeScale = 1f;
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    
        public void ChangeScene(int nameScene)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nameScene);
        }
        public void ReturnHome()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Home");
        }
        public void ReturnMenuMap()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MenuMap");
        }
        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        public void Pause(bool pause){
            Time.timeScale = pause ? 0f : 1f;
            Debug.Log($"pause {pause}");
        }
    }
}
