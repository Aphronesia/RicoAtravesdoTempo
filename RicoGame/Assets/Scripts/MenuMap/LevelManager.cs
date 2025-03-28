using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Rico;
    public List<GameObject> levels = new List<GameObject>();
    [SerializeField]
    private int levelCount;
    
    public static event Action OnStart;
    private void Start() {
        GetLevels();
        OnStart?.Invoke();
    }
    private void GetLevels(){
        for(int i = 0; i < levelCount; i++){
            levels.Add(GameObject.Find("Level" + i));
            Debug.Log("achou level" + i);
        }

    }
}
