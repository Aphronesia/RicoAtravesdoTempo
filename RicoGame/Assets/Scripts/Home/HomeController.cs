using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : MonoBehaviour
{
    public ControlScenes controlScenes;
    public int NextScene;
    [SerializeField]
    private GameObject credits;
    private bool credi;
    void Start()
    {
        ControlScenes ScenesController = FindObjectOfType<ControlScenes>();
        if (ScenesController != null){controlScenes = ScenesController.GetComponent<ControlScenes>();}
        else {Debug.LogError("Objeto indestrutível não encontrado!");}
        //verifica se a string NextScene ta vazia, e bom pq posso no futuro usar isso pra fazer o jogo comecar na ultima cena salva
        if (NextScene == 0)
        {
            NextScene = 1;
        }
        //controla rotacao de tela (muda pra retrato)
        Screen.orientation = ScreenOrientation.Portrait;
        credits.SetActive(false);
        credi = false;
        
    }
    public void StartGame()
    {
        controlScenes.ChangeScene(NextScene);
    }
    public void ChangeScene(int sceneNumber){
        controlScenes.ChangeScene(sceneNumber);
    }
    public void QuitGame()
    {
        controlScenes.QuitGame();
    }
    public void ShowCredits()
    {
        if(credi)
        {
            credits.SetActive(false);
            credi = false;
        }
        else
        {
            credits.SetActive(true);
            credi = true;
        }
    }
}
