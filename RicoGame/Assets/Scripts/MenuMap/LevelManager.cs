using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MenuMap{
    public class LevelManager : MonoBehaviour
    {
        private Game.SaveLoadSystem _saveLoadSystem;
        private Vector3 target;
        public int actualRico = 0;
        [SerializeField]
        private int levelCount;
        private LevelPopUp lvPop;
        public static event Action OnStart;
        public static event Action<Vector3> OnTarget;
        public static event Action<Vector3> OnTargetLoad;
        public List<Level> levels = new List<Level>();
        [System.Serializable]
        public class Level
        {
            public GameObject objLevel;
            public bool played;
        }

        private void OnEnable(){
            Game.SaveLoadSystem.OnLoadGame += OnLoad;
        }

        private void OnDisable(){
            Game.SaveLoadSystem.OnLoadGame -= OnLoad;
        }

        private void OnLoad(){
            actualRico = _saveLoadSystem.gameData.menuMapRico;
            target = levels[actualRico].objLevel.transform.position;
            OnTargetLoad?.Invoke(target);
            
        }
        private void Start() {
            GameObject levelPop = GameObject.Find("Canvas/LevelMenu");
            if(levelPop !=null){
                lvPop = levelPop.GetComponent<LevelPopUp>();
            }
            GameObject saveLoadManager = GameObject.Find("SaveLoadManager");
            if (saveLoadManager != null){
                _saveLoadSystem = saveLoadManager.GetComponent<Game.SaveLoadSystem>();
            }
            GetLevels();
            OnStart?.Invoke();
            target = levels[0].objLevel.transform.position;
            OnTarget?.Invoke(target);
        }
        private void GetLevels(){
            for(int i = 0; i < levelCount; i++){
                GameObject levelFind = GameObject.Find("Level" + i);
                if (levelFind != null){
                    levels.Add(new Level ());
                    levels[i].objLevel = levelFind;
                    levels[i].played = true; //false
                }
            }
            levels[0].played = true;
            levels[1].played = true;
        }
        private void Update() {
            GetClick();
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
                    //Debug.Log("a");/
                    GameObject objectClicked = collider.gameObject;
                    GetLevel(objectClicked);
                }
            }
        }
        private void GetLevel(GameObject objClick){
            int index = levels.FindIndex(Level => Level.objLevel == objClick);
            //resulta em -1 se o obj clicado nao tiver na List

            if(index != -1 && levels[index].played == true ){
                if ((index + 1) == actualRico || (index - 1) == actualRico)
                {
                    target = levels[index].objLevel.transform.position;
                    OnTarget?.Invoke(target);
                    actualRico = index;
                    _saveLoadSystem.gameData.menuMapRico = actualRico;
                    lvPop.LevelExit();
                }
            }
        }
    }
}

