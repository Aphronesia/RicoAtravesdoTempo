using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    void Start()
    {
        
    }
    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    
    public void ChangeScene(int NameScene)
    {
        //Debug.Log("mudou a cena");
        SceneManager.LoadScene(NameScene);
    }
    public void SartTrem()
    {
        //Debug.Log("mudou a cena");
        SceneManager.LoadScene(2);
    }
    public void ReturnHome()
    {
        SceneManager.LoadScene("Home");
    }
    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
