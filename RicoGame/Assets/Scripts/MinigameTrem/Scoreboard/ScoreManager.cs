using System;
using System.Collections;
using Game;
using TMPro;
using UnityEngine;

namespace MinigameTrem.Scoreboard{
    public class ScoreManager : MonoBehaviour
    {
        private int _points;
        [SerializeField]
        private int pointsToWin;
        private string txtPoints = "Pontos: ";
        public float tempoAcumulado;
        public bool poinsIsRunning;
        public TMP_Text timeText;
        
        private ControleUITrem _controleUI;
        private SaveLoadSystem _saveLoadSystem;

        public static event Action OnGanhou;
        
        // Start is called before the first frame update
        void Start(){
            TakeComponents();
            _points = 0;
            tempoAcumulado = 0f;
            poinsIsRunning = false;
        }
        public void StartRun()
        {
            poinsIsRunning = true;
        }
        public void StopRun()
        {
            poinsIsRunning = false;
        }
        void Update()
        {
            if(poinsIsRunning)
            {
                tempoAcumulado += Time.deltaTime;  // Soma o tempo decorrido
                if (tempoAcumulado >= 0.1f)  // Verifica se 1 segundo se passou
                {
                    _points++;
                    tempoAcumulado = 0f;     // Reseta o contador
                }
                timeText.text = txtPoints + _points.ToString();
            }
            
            // VITÓRIA 
            if (_points >= pointsToWin){
                //Debug.Log("ganhouuuu");
                StopRun();
                OnGanhou?.Invoke();
                if (_points > _saveLoadSystem.runtimeGameData.recordPoinsTrem){
                    _saveLoadSystem.runtimeGameData.recordPoinsTrem = _points;
                }
            }
        }
        private void TakeComponents(){
            GameObject saveLoadManager =  GameObject.Find("SaveLoadManager");
            if (saveLoadManager != null){
                _saveLoadSystem = saveLoadManager.GetComponent<SaveLoadSystem>();
            }

            ControleUITrem controleUITrem = FindObjectOfType<ControleUITrem>();
            if (controleUITrem != null){
                _controleUI = controleUITrem.GetComponent<ControleUITrem>();
            }
            
        }
    }
}
