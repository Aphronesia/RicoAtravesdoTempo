using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleUITrem : MonoBehaviour
{
    [SerializeField]
    private GameObject StartUI, MorreuUI, PauseUI;
    public CapivaraTrem capivaratrem;
    public ControlScenes controlScenes;
    public Monstros monstros;
    public Estrelas estrelas;

    bool paused;
    // Start is called before the first frame update
    void Start()
    {
        MorreuUI.SetActive(false);
        StartUI.SetActive(true);
        //procura o controlador de cenas
        ControlScenes ScenesController = FindObjectOfType<ControlScenes>();
        if (ScenesController != null) { controlScenes = ScenesController.GetComponent<ControlScenes>(); }
        else { Debug.LogError("Objeto indestrutível não encontrado!"); }
        //controla rotacao de tela (muda prapaisagem)
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    //click do play inicial do jogo
    public void StartGame()
    {
        //Debug.Log("ControleUI iniciou");
        StartUI.SetActive(false);
        capivaratrem.StartGame();
        monstros.StartGame();
        //scoreManager.StartRun();
        estrelas.canMove = true;
    }
    IEnumerator Faleceu()
    {
        yield return new WaitForSeconds(1.5f);
        MorreuUI.SetActive(true);
    }
    public void Morreu()
    {
        //scoreManager.StopRun();
        StartCoroutine(Faleceu());
    }
    public void Pause()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0f;
            PauseUI.SetActive(true);
            //scoreManager.StopRun();
        }
        else
        {
            paused = false;
            Time.timeScale = 1f;
            PauseUI.SetActive(false);
            //scoreManager.StartRun();
        }
    }
    public void Recomeca()
    {
        controlScenes.RestartGame();
    }
    public void QuitGame()
    {
        controlScenes.QuitGame();
    }
    public void ReturnHome()
    {
        controlScenes.ReturnHome();
    }
    public void ReturnMenuMap(){
        controlScenes.ReturnMenuMap();
    }
}
