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
        private Game.ControlSounds _sound;
        private Game.SaveLoadSystem _saveLoad;
        public MusicControl music;

        public static event Action OnStopMusic;
        public static event Action<int> OnLevel;
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
            if (_sound != null)
            {
                _sound.StopMusic();
            }
            else
            {
                OnStopMusic?.Invoke();
            }
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
            OnLevel?.Invoke(4);
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
        [SerializeField] private float durationFade;
        [SerializeField] private CanvasGroup UITransition; 
        private IEnumerator Transition(int value)
        {
            float startAlpha = UITransition.alpha;
            float time = 0f;
            while (time < durationFade){
                time += Time.deltaTime;
                UITransition.alpha = Mathf.Lerp(startAlpha, 1f, time / durationFade);
                yield return null;
            }
            UITransition.alpha = 1f;
            UITransition.interactable = true;
            UITransition.blocksRaycasts = true;
            _scene.ChangeScene(value); // menu map
        }
        
        private void TakeComponents()
        {
            var sceneObj = FindObjectOfType<Game.ControlScenes>();
            if ( sceneObj != null)
                _scene = sceneObj.GetComponent<Game.ControlScenes>();
            var saveObj = FindAnyObjectByType<Game.SaveLoadSystem>();
            if ( saveObj != null)
                _saveLoad = saveObj.GetComponent<SaveLoadSystem>();
            var soundObj = FindAnyObjectByType<Game.ControlSounds>();
            if ( soundObj != null)
                _sound = saveObj.GetComponent<ControlSounds>();
        }
    }
}
