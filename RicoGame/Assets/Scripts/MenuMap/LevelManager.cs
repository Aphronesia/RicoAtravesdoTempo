using System;
using System.Collections.Generic;
using UnityEngine;

namespace MenuMap{
    public class LevelManager : MonoBehaviour
    {
        private Game.SaveLoadSystem _saveLoadSystem;

        [SerializeField] private RicoMove ricomove;
        private Vector3 _target;
        public int actualRico;
        [SerializeField]
        private int levelCount;
        private LevelPopUp _lvPop;
        public static event Action<Vector3, bool> OnTarget;
        public List<Level> levels = new List<Level>();
        [Serializable]
        public class Level
        {
            public GameObject objLevel;
            public bool played;
        }
        private void Load(){
            actualRico = _saveLoadSystem.runtimeGameData.menuMapRico;
            _target = levels[actualRico].objLevel.transform.position;
            
        }
        private void Start(){
            TakeComponent();
            
            GetLevels();

            
            Load();
            OnTarget?.Invoke(_target, true);
            return;
            
            //_target = levels[0].objLevel.transform.position;
            //OnTarget?.Invoke(_target, true);
            
        }
        private void GetLevels(){
            for(int i = 0; i < levelCount; i++){
                GameObject levelFind = GameObject.Find("Level" + i);
                if (levelFind != null){
                    levels.Add(new Level ());
                    levels[i].objLevel = levelFind;
                    levels[i].played = false; //false
                }
            }
            levels[0].played = true;
            
                for (int i = 0; i <= _saveLoadSystem.runtimeGameData.levelCompleted; i++){
                    levels[i].played = true;
                }
                return;
            
            //levels[1].played = true;
        }
        private void Update() {
            GetClick();
        }
        private void GetClick(){
            if (Input.touchCount > 0){
                Touch touch = Input.GetTouch(0); //pega o primeiro click na tela
                if (touch.phase == TouchPhase.Began){
                    if (Camera.main == null){
                        return;
                    }
                    Vector2 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
                    Collider2D colliderPoint = Physics2D.OverlapPoint(worldPoint);
                    if (colliderPoint != null){
                        Debug.Log("a");
                        GameObject objectClicked = colliderPoint.gameObject;
                        GetLevel(objectClicked);
                    }
                }
            }
            if (Input.GetMouseButtonDown(0)){
                if (Camera.main == null){
                    return;
                }
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D colliderPoint = Physics2D.OverlapPoint(worldPoint);
                if (colliderPoint != null){
                    //Debug.Log("a");/
                    GameObject objectClicked = colliderPoint.gameObject;
                    GetLevel(objectClicked);
                }
            }
        }
        private void GetLevel(GameObject objClick){
            int index = levels.FindIndex(level => level.objLevel == objClick);
            //resulta em negativo se o obj clicado nao tiver na List

            if(index != -1 && levels[index].played ){
                if ((index + 1) == actualRico || (index - 1) == actualRico)
                {
                    _target = levels[index].objLevel.transform.position;
                    OnTarget?.Invoke(_target, false);
                    actualRico = index;
                    _saveLoadSystem.runtimeGameData.menuMapRico = actualRico;
                    _lvPop.LevelExit();
                }
            }
        }

        public void NextorPrevLevel(bool direction){
            if (ricomove.moving){
                return;
            }
            int indexNext = direction ? actualRico + 1 : actualRico - 1;
            indexNext = Mathf.Clamp(indexNext, 0, levelCount);
            if (levels[indexNext].played){
                GameObject objLevel = levels[indexNext].objLevel;
                GetLevel(objLevel);
            }
        }
        private void TakeComponent(){
            GameObject levelPop = GameObject.Find("Canvas/LevelMenu");
            if(levelPop !=null){
                _lvPop = levelPop.GetComponent<LevelPopUp>();
            }
            GameObject saveLoadManager = GameObject.Find("SaveLoadManager");
            if (saveLoadManager != null){
                _saveLoadSystem = saveLoadManager.GetComponent<Game.SaveLoadSystem>();
            }
        }
    }
}

