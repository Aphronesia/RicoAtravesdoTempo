using System;
using TMPro;
using UnityEngine;

namespace MinigameRitmo.UI
{
    public class UIControl : MonoBehaviour
    {
        [Header("Interfaces")] 
        public GameObject tutorial;
        public GameObject gameOver;
        public GameObject gameWin;
        
        
        [Header("Properties")]
        [SerializeField] private TextMeshProUGUI pontostxt;

        [SerializeField] private TextMeshProUGUI combotxt;
        
        private Game.ControlScenes _scene;
        public MusicControl music;
        private void Start()
        {
            TakeComponents();
            _scene.Pause(true);
            RefreshTextCombos(0);
            RefreshTextPontos(0);
        }

        public void PlayGame()
        {
            _scene.Pause(false);
            tutorial.SetActive(false);
            music.PlayMusic();
        }

        public void EndGame()
        {
            Debug.Log("Acabooou");
        }
        public void RefreshTextPontos(int value)
        {
            pontostxt.text = $"pontos: {value}";
        }

        public void RefreshTextCombos(int value)
        {
            combotxt.text = $"Combo: {value}";
        }

        private void TakeComponents()
        {
            var sceneObj = FindObjectOfType<Game.ControlScenes>();
            if ( sceneObj != null)
                _scene = sceneObj.GetComponent<Game.ControlScenes>();
        }
    }
}
