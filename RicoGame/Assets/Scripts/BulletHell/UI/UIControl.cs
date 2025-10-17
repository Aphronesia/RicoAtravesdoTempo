using System;
using System.Collections;
using BulletHell.Player;
using Game;
using UnityEngine;
using UnityEngine.UI;
//para eventos estaticos

namespace BulletHell.UI{
    public class UIControl : MonoBehaviour
    {
        private ControlScenes controlScenes;
        private Game.SaveLoadSystem saveLoadSystem;
        [SerializeField] private float secondsToEnd;
        [SerializeField]
        private GameObject CtrlScenes, uiStart, uiPause, uiDead, uiWin;
        public bool running, paused;
        [SerializeField]
        private Sprite atkBttnOn, atkBttnOff;
        [SerializeField]
        private Image AtkButton;
        public static event Action<bool> OnStarted;
        private void OnEnable() {
            EnemyControl.OnEnemyTired += EnemyTired;
            Player_Status.OnPlayeDeath += PlayerDie;
            EnemyStatus.OnEnemyDie += Enemydie;
        }
        private void OnDisable() {
            EnemyControl.OnEnemyTired -= EnemyTired;
            Player_Status.OnPlayeDeath -= PlayerDie;
            EnemyStatus.OnEnemyDie -= Enemydie;
        }
        private void EnemyTired(bool value){
            if (value){
                AtkButton.sprite = atkBttnOn;
            }
            else{
                AtkButton.sprite = atkBttnOff;
            }
        }
        private void GetUIObjects(){
            uiStart = GameObject.Find("Canvas/UIStart");
            uiPause = GameObject.Find("Canvas/UIPause");
            uiDead = GameObject.Find("Canvas/UIDead");
            uiWin = GameObject.Find("Canvas/UIWin");
        }
        private void Start()
        {
            TakeComponents();
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            GetUIObjects();
            if (uiStart != null){
                uiStart.SetActive(true);
            }
            if (uiPause != null){
                uiPause.SetActive(false);
                paused = false;
                controlScenes.Pause(paused);
            }
            if (uiDead != null){
                uiDead.SetActive(false);
            }

            if (uiWin != null)
            {
                uiWin.SetActive(false);
            }
            running = false;
        }
        //quando botão de play for apertado
        public void Started(){
            running = true;
            uiStart.SetActive(false);
            OnStarted?.Invoke(running);
        }
        public void PlayerDie(){
            uiDead.SetActive(true);
            controlScenes.Pause(true);
        }

        public void Enemydie()
        {
            StartCoroutine(GameWin());
            
        }
        IEnumerator GameWin()
        {
            yield return new WaitForSeconds(secondsToEnd);
            Win();
            
        }
        public void Win()
        {
            if(saveLoadSystem.runtimeGameData.levelCompleted <= 3)
                saveLoadSystem.runtimeGameData.levelCompleted = 3;
            uiWin.SetActive(true);
            uiDead.SetActive(false);
            saveLoadSystem.runtimeGameData.menuMapRico = 2;
        }
        public void Pause(){
            paused =! paused;
            uiPause.SetActive(paused);
            controlScenes.Pause(paused);
        }
        public void Restart(){
            controlScenes.RestartGame();
        }
        public void Home(){
            controlScenes.ReturnHome();
        }

        public void MenuMap()
        {
            controlScenes.ReturnMenuMap();
        }
        public void Quit(){
            controlScenes.QuitGame();
        }

        private void TakeComponents()
        {
            var sceneObj = FindObjectOfType<Game.ControlScenes>();
            if ( sceneObj != null)
                controlScenes = sceneObj.GetComponent<Game.ControlScenes>();
            var saveObj = FindAnyObjectByType<Game.SaveLoadSystem>();
            if ( saveObj != null)
                saveLoadSystem = saveObj.GetComponent<SaveLoadSystem>();
        }
    }
}
