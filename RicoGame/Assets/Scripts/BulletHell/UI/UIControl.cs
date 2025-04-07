using System; //para eventos estaticos
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    private ControlScenes controlScenes;
    [SerializeField]
    private GameObject CtrlScenes, uiStart, uiPause, uiDead;
    public bool running, paused;
    [SerializeField]
    private Sprite atkBttnOn, atkBttnOff;
    [SerializeField]
    private Image AtkButton;
    public static event Action<bool> OnStarted;
    private void OnEnable() {
        EnemyControl.OnEnemyTired += EnemyTired;
        Player_Status.OnPlayeDeath += PlayerDie;
    }
    private void OnDisable() {
        EnemyControl.OnEnemyTired -= EnemyTired;
        Player_Status.OnPlayeDeath -= PlayerDie;
    }
    private void EnemyTired(bool value){
        if (value){
            AtkButton.sprite = atkBttnOn;
        }
        else{
            AtkButton.sprite = atkBttnOff;
        }
    }
    private void GetUIObjects(){
        uiStart = GameObject.Find("Canvas/UIStart");
        uiPause = GameObject.Find("Canvas/UIPause");
        uiDead = GameObject.Find("Canvas/UIDead");
    }
    private void Start() {
        controlScenes = CtrlScenes.GetComponent<ControlScenes>();
        GetUIObjects();
        if (uiStart != null){
            uiStart.SetActive(true);
        }
        if (uiPause != null){
            uiPause.SetActive(false);
            paused = false;
            controlScenes.Pause(paused);
        }
        if (uiDead != null){
            uiDead.SetActive(false);
        }
        running = false;
    }
    //quando botão de play for apertado
    public void Started(){
        running = true;
        uiStart.SetActive(false);
        OnStarted?.Invoke(running);
    }
    public void PlayerDie(){
        uiDead.SetActive(true);
        controlScenes.Pause(true);
    }
    public void Pause(){
        paused =! paused;
        uiPause.SetActive(paused);
        controlScenes.Pause(paused);
    }
    public void Restart(){
        controlScenes.RestartGame();
    }
    public void Home(){
        controlScenes.ReturnHome();
    }
    public void Quit(){
        controlScenes.QuitGame();
    }
}
