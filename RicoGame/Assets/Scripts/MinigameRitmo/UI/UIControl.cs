using System;
using TMPro;
using UnityEngine;
using System.Collections;
using Game;

namespace MinigameRitmo.UI
{
    public class UIControl : MonoBehaviour
    {
        [Header("Interfaces")] 
        public GameObject tutorial;
        public GameObject gameOver;
        public GameObject gameWin;
        
        
        [Header("Properties")]
        [SerializeField] private float secondsToEnd;
        [SerializeField] private TextMeshProUGUI pontostxt;

        [SerializeField] private TextMeshProUGUI combotxt;

        [SerializeField] private Score score;
        private Game.ControlScenes _scene;
        private Game.SaveLoadSystem _saveLoad;
        public MusicControl music;

        private void OnEnable()
        {
            RitmoControl.OnEndMusic += EndGame;
        }

        private void OnDisable()
        {
            RitmoControl.OnEndMusic -= EndGame;
        }
        
        private void Start()
        {
            TakeComponents();
            _scene.Pause(true);
            RefreshTextCombos(0);
            RefreshTextPontos(0);
            
            tutorial.SetActive(true);
            gameWin.SetActive(false);
            gameOver.SetActive(false);
        }

        public void PlayGame()
        {
            _scene.Pause(false);
            tutorial.SetActive(false);
            music.PlayMusic();
        }

        public void EndGame()
        {
            StartCoroutine(IEnd(score.GetScore()));
        }

        IEnumerator IEnd(bool state)
        {
            yield return new WaitForSeconds(secondsToEnd);
            if (state)
            {
                Win();
            }
            else
            {
                gameOver.SetActive(true);
                gameWin.SetActive(false);
            }
            _saveLoad.runtimeGameData.menuMapRico = 3;
        }

        public void Win()
        {
            if(_saveLoad.runtimeGameData.levelCompleted <= 4)
                _saveLoad.runtimeGameData.levelCompleted = 4;
            gameWin.SetActive(true);
            gameOver.SetActive(false);
            _saveLoad.runtimeGameData.menuMapRico = 3;
        }
        public void RefreshTextPontos(int value)
        {
            pontostxt.text = $"pontos: {value}";
        }

        public void RefreshTextCombos(int value)
        {
            combotxt.text = $"Combo: {value}";
        }

        public void GoToMenu()
        {
            _scene.ReturnMenuMap();
        }

        public void RestartGame()
        {
            _scene.RestartGame();
        }
        private void TakeComponents()
        {
            var sceneObj = FindObjectOfType<Game.ControlScenes>();
            if ( sceneObj != null)
                _scene = sceneObj.GetComponent<Game.ControlScenes>();
            var saveObj = FindAnyObjectByType<Game.SaveLoadSystem>();
            if ( saveObj != null)
                _saveLoad = saveObj.GetComponent<SaveLoadSystem>();
        }
    }
}
