using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicoMove : MonoBehaviour
{
    
    private LevelManager levelManager;
    private void OnEnable() {
        LevelManager.OnStart += Started;
    }
    private void OnDisable() {
        LevelManager.OnStart -= Started;
    }
    private void Start() {
        GameObject levelController = GameObject.Find("LevelController");
        if (levelController != null){
            levelManager = levelController.GetComponent<LevelManager>();
        }
    }
    public void Started(){
        Vector3 levelZeroPos = levelManager.levels[0].transform.position;
        if (levelZeroPos != null){
            transform.position = levelZeroPos;
        }
    }
}
