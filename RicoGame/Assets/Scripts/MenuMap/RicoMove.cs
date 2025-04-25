using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicoMove : MonoBehaviour
{
    [Header("Atributos do movimento")]
    private LevelManager lvManager;
    [SerializeField]
    private LevelPopUp lvPop;
    private Rigidbody2D rig;
    private Vector3 target;
    [SerializeField, Tooltip("velocidade do movimento")]
    private float speed;
    private void OnEnable() {
        LevelManager.OnStart += Started;
        LevelManager.OnTarget += Moviment;
    }
    private void OnDisable() {
        LevelManager.OnStart -= Started;
        LevelManager.OnTarget -= Moviment;
    }
    private void Start() {
        GameObject levelController = GameObject.Find("LevelController");
        if (levelController != null){
            lvManager = levelController.GetComponent<LevelManager>();
        }
        rig = GetComponent<Rigidbody2D>();
    }
    public void Started(){
        Vector3 levelZeroPos = lvManager.levels[0].objLevel.transform.position;
        if (levelZeroPos != null){
            transform.position = levelZeroPos;
        }
    }
    private void FixedUpdate() {
        //transform.position = Vector3.MoveTowards(transform.position, target, 0.5f);
        Vector3 currentPos = rig.position;
        

        Vector3 direction = target - currentPos;
        float distance = direction.magnitude;
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
    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.gameObject.tag){
            case "Level":
                //Debug.Log(other.gameObject.name);
                lvPop.LevelEnter(lvManager.actualRico);
            break;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        switch(other.gameObject.tag){
            case "Level":
                //Debug.Log(other.gameObject.name);
                lvPop.LevelExit();
            break;
        }
    }
}
