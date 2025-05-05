using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class configUI : MonoBehaviour
{
    [SerializeField]
    private GameObject UIMenuPause;
    private RectTransform _pauseRectTransform;
    [SerializeField]
    private Vector3 outPosition;



    [SerializeField]
    private bool pause = false;
    private ControlScenes _controlScenes;
    public static event Action<bool> OnPause;
    
    private void OnEnable() {
        
    }
    private void OnDisable() {
        
    }
    private void Start() {
        UIMenuPause = GameObject.Find("UIMenuPause");
        if(UIMenuPause != null){
            _pauseRectTransform = UIMenuPause.GetComponent<RectTransform>()
;        }
        ControlScenes ScenesController = FindObjectOfType<ControlScenes>();
        if (ScenesController != null){
            _controlScenes = ScenesController.GetComponent<ControlScenes>();
        }
    }
    public void Pause(){
        pause =! pause;
        OnPause?.Invoke(pause);

        if(pause){
            _pauseRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            _pauseRectTransform.anchorMax = new Vector2(0.5f, 0.5f);

            _pauseRectTransform.pivot = new Vector2(0.5f, 0.5f);
            _pauseRectTransform.anchoredPosition = Vector2.zero;
        }
        else{
            _pauseRectTransform.pivot = outPosition;
        }
    }
}
