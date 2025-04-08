using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Rico;
    private GameObject target;
    private int actualRico;
    public List<GameObject> levels = new List<GameObject>();
    [SerializeField]
    private int levelCount;
    
    public static event Action OnStart;
    private void Start() {
        actualRico = 0;
        GetLevels();
        OnStart?.Invoke();
        target = levels[0];

    }
    private void GetLevels(){
        for(int i = 0; i < levelCount; i++){
            levels.Add(GameObject.Find("Level" + i));
        }
    }
    private void Update() {
        GetClick();
    }
    private void FixedUpdate() {
        Rico.transform.position = Vector3.MoveTowards(Rico.transform.position, target.transform.position, 0.5f);
    }
    private void GetClick(){
        if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0); //pega o primeiro click na tela
            if (touch.phase == TouchPhase.Began){
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
                Collider2D collider = Physics2D.OverlapPoint(worldPoint);
                if (collider != null){
                    Debug.Log("a");
                    GameObject objectClicked = collider.gameObject;
                    GetLevel(objectClicked);
                }
            }
        }
        if (Input.GetMouseButtonDown(0)){
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.OverlapPoint(worldPoint);
            if (collider != null){
                //Debug.Log("a");
                GameObject objectClicked = collider.gameObject;
                GetLevel(objectClicked);
            }
        }
    }
    private void GetLevel(GameObject objClick){
        int index = levels.IndexOf(objClick);
        if(index != -1){
            if ((index + 1) == actualRico || (index - 1) == actualRico){
                target = levels[index];
                actualRico = index;
            }
        }
    }
}
