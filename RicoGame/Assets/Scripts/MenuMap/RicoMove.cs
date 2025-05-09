using System;
using System.Collections;
using System.Collections.Generic;
using MenuMap;
using UnityEngine;

public class RicoMove : MonoBehaviour
{
    [Header("Atributos do movimento")]
    private LevelManager lvManager;
    [SerializeField]
    private LevelPopUp lvPop;
    public Rigidbody2D rig;
    private Vector3 target;
    [SerializeField, Tooltip("velocidade do movimento")]
    private float speed;
    private bool moving;

    public float distance;
    private void OnEnable() {
        LevelManager.OnStart += Started;
        LevelManager.OnTarget += Moviment;
        LevelManager.OnTargetLoad += LoadPosition;
    }
    private void OnDisable() {
        LevelManager.OnStart -= Started;
        LevelManager.OnTarget -= Moviment;
        LevelManager.OnTargetLoad -= LoadPosition;
    }
    private void Start() {
        GameObject levelController = GameObject.Find("LevelController");
        if (levelController != null){
            lvManager = levelController.GetComponent<LevelManager>();
        }
        GameObject levelPop = GameObject.Find("Canvas/LevelMenu");
        if(levelPop !=null){
            lvPop = levelPop.GetComponent<LevelPopUp>();
        }
        rig = GetComponent<Rigidbody2D>();
        
        moving = true;
    }
    public void Started(){
        Vector3 levelZeroPos = lvManager.levels[0].objLevel.transform.position;
        if (levelZeroPos != null){
            transform.position = levelZeroPos;
        }
    }
    private void Update() {
        if (moving && distance == 0){
            lvPop.LevelEnter(lvManager.actualRico);
            moving = false;
            //Debug.Log("parou");
        }
        if (!moving && distance != 0){
            lvPop.LevelExit();
            moving = true;
            //Debug.Log("movendo");
        }
    }
    private void FixedUpdate() {
        //transform.position = Vector3.MoveTowards(transform.position, target, 0.5f);
        Vector3 currentPos = rig.position;
        

        Vector3 direction = target - currentPos;
        distance = direction.magnitude;
        float step = speed * Time.fixedDeltaTime;
        if(distance <= step){
            rig.MovePosition(target);
            rig.velocity = Vector3.zero;
        } else {
            Vector3 newPos = currentPos + (direction / distance) * step;
            rig.MovePosition(newPos);
        }
    }
    private void Moviment(Vector3 newTarget){
        target = newTarget;
    }

    private void LoadPosition(Vector3 target){
        transform.position = target;
    }
}
