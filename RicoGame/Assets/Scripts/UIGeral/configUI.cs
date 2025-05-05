using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class configUI : MonoBehaviour
{
    [SerializeField]
    private GameObject UIMenuPause;
    [SerializeField]
    private bool pause;
    private ControlScenes _controlScenes;
    public static event Action<bool> OnPause;
    
    private void OnEnable() {
        
    }
    private void OnDisable() {
        
    }
    private void Start() {
        UIMenuPause = GameObject.Find("UIMenuPause");
        
        ControlScenes ScenesController = FindObjectOfType<ControlScenes>();
        if (ScenesController != null){
            _controlScenes = ScenesController.GetComponent<ControlScenes>();
        }
    }
    public void Pause(){
        pause =! pause;
        OnPause?.Invoke(pause);
    }
}
