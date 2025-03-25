using System; //para eventos estaticos
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    private ControlScenes controlScenes;
    [SerializeField]
    private GameObject uiStart, uiPause;
    public bool running, paused;
    [SerializeField]
    private Sprite atkBttnOn, atkBttnOff;
    [SerializeField]
    private Image AtkButton;
    public static event Action<bool> OnStarted;
    private void OnEnable() {
        EnemyControl.OnEnemyTired += EnemyTired;
    }
    private void OnDisable() {
        EnemyControl.OnEnemyTired -= EnemyTired;
    }
    private void EnemyTired(bool value){
        if (value){
            AtkButton.sprite = atkBttnOn;
        }
        else{
            AtkButton.sprite = atkBttnOff;
        }
    }
    private void Start() {
        uiStart = GameObject.Find("Canvas/UIStart");
        GameObject CtrlScenes = GameObject.Find("ScenesController");
        if (CtrlScenes != null){
            controlScenes = CtrlScenes.GetComponent<ControlScenes>();
        }
        if (uiStart != null){
            uiStart.SetActive(true);
        }
        running = false;
    }
    //quando botão de play for apertado
    public void Started(){
        running = true;
        uiStart.SetActive(false);
        OnStarted?.Invoke(running);
    }
    public void UIPause(){
        controlScenes.Pause(paused);
    }
    public void UIRestart(){

    }
    public void UIQuit(){

    }
}
