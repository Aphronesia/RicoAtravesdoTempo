using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlUIMap : MonoBehaviour
{
    [SerializeField]
    private LevelPopUp lvPop;
    private ControlScenes controlScenes;
    private LevelManager lvManager;
    private void Start() {
        ControlScenes ScenesController = FindObjectOfType<ControlScenes>();
        if (ScenesController != null){
            controlScenes = ScenesController.GetComponent<ControlScenes>();
        }
        GameObject levelController = GameObject.Find("LevelController");
        if (levelController != null){
            lvManager = levelController.GetComponent<LevelManager>();
        }
        GameObject levelPop = GameObject.Find("Canvas/LevelMenu");
        if(levelPop !=null){
            lvPop = levelPop.GetComponent<LevelPopUp>();
        }
    }
    public void PlayLevel(){
        int index = lvPop.SceneIndex();
        controlScenes.ChangeScene(index);
    }
}
