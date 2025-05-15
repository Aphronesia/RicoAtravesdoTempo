using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class HomeControl : MonoBehaviour
{
    public ControlScenes controlScenes;
    private Game.SaveLoadSystem saveLoadSystem;
    private void Start() {
        ControlScenes ScenesController = FindObjectOfType<ControlScenes>();
        if (ScenesController != null){
            controlScenes = ScenesController.GetComponent<ControlScenes>();
        }
        Game.SaveLoadSystem saveLoader = FindObjectOfType<Game.SaveLoadSystem>();
        if (saveLoader != null){
            saveLoadSystem = saveLoader.GetComponent<Game.SaveLoadSystem>();
        }
        //controla rotacao de tela (muda pra retrato)
        Screen.orientation = ScreenOrientation.Portrait;
    }
    private void StartNewGame(){

    }
    private void ContinueGame(){

    }
    private void Quit(){
        controlScenes.QuitGame();
    }
}
