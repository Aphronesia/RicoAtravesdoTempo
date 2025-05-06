using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class configUI : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    private bool pause = false;

    [Header("Menu UI")]
    [SerializeField]
    private GameObject UIMenuPause;
    private RectTransform _menuRt;
    [SerializeField]
    private Vector3 menuOutPos;
    [SerializeField]
    private float durationMoveUI;

    private ControlScenes _controlScenes;

    public static event Action<bool> OnPause;
    
    private void OnEnable() {
        
    }
    private void OnDisable() {
        
    }
    private void Start() {
        UIMenuPause = GameObject.Find("UIMenuPause");
        if(UIMenuPause != null){
            _menuRt = UIMenuPause.GetComponent<RectTransform>()
;        }
        ControlScenes ScenesController = FindObjectOfType<ControlScenes>();
        if (ScenesController != null){
            _controlScenes = ScenesController.GetComponent<ControlScenes>();
        }
    }
    public void Home(){
        _controlScenes.ReturnHome();
    }
    public void Quit(){
        _controlScenes.QuitGame();
    }
    public void Pause(){
        pause =! pause;
        OnPause?.Invoke(pause);

        if(pause){
            //Centralizar o menu de Pause
            _menuRt.anchorMin = new Vector2(0.5f, 0.5f);
            _menuRt.anchorMax = new Vector2(0.5f, 0.5f);
            _menuRt.pivot = new Vector2(0.5f, 0.5f);
            //_menuRt.anchoredPosition = Vector2.zero;
            StartCoroutine(MoveMenuUI(Vector3.zero));
        }
        else{
            _controlScenes.Pause(pause);
            //_menuRt.anchoredPosition = menuOutPos;
            StartCoroutine(MoveMenuUI(menuOutPos));
        }

        IEnumerator MoveMenuUI(Vector3 endPos){
            Vector3 starPos = _menuRt.anchoredPosition;

            float elapsed  = 0f;
            while (elapsed < durationMoveUI){
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01 (elapsed /durationMoveUI);
                _menuRt.anchoredPosition = Vector3.Slerp(starPos, endPos, t);
                yield return null;
            }
            _menuRt.anchoredPosition = endPos;
            _controlScenes.Pause(pause);
            //o jogo vai parar só depois que o movimento for concluido
            //RESOLVER ISSO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

    }
}
