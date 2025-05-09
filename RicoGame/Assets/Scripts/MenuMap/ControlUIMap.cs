using System.Collections;
using System.Collections.Generic;
using MenuMap;
using UnityEngine;

public class ControlUIMap : MonoBehaviour
{
    [SerializeField]
    private LevelPopUp lvPop;
    private ControlScenes _controlScenes;
    private LevelManager _lvManager;

    private bool _pause;
    private void Start() {
        ControlScenes ScenesController = FindObjectOfType<ControlScenes>();
        if (ScenesController != null){
            _controlScenes = ScenesController.GetComponent<ControlScenes>();
        }
        GameObject levelController = GameObject.Find("LevelController");
        if (levelController != null){
            _lvManager = levelController.GetComponent<LevelManager>();
        }
        GameObject levelPop = GameObject.Find("Canvas/LevelMenu");
        if(levelPop !=null){
            lvPop = levelPop.GetComponent<LevelPopUp>();
        }
    }
    public void PlayLevel(){
        int index = lvPop.SceneIndex();
        _controlScenes.ChangeScene(index);
    }
}
