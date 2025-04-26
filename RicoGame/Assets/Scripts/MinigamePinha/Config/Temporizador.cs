using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Temporizador : MonoBehaviour
{
    public float timeRemaining = 120;
    public bool timerIsRunning = false;
    public TMP_Text timeText;
    
    public SpawnCai spawnCai;


    public TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
       // Time.timeScale=1f;
        timerIsRunning = false;
        DisplayTime(timeRemaining);
    }
    public void Comeca(){
        timerIsRunning = true;
        //timeRemaining = 120;
    }
    public void Parar(){
        timerIsRunning = false;
    }
    public void Voltar(){
        timerIsRunning = true;
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("tempo acabou");
                spawnCai.Tempofim();
                Time.timeScale=0f;
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
        switch (timeRemaining){
            case <=30:
            spawnCai.TempoRapido();
            break;
            case >30 and <=90:
            spawnCai.TempoMedio();
            break;
            default:
            spawnCai.TempoPadrao();
            break;
        }
    }
    public void Dano(){
        StartCoroutine(CorDano());
    }
    IEnumerator CorDano()
    {
        textMeshProUGUI.color = Color.red;
        if ( timeRemaining > 10f){
            timeRemaining = timeRemaining - 10f;
            DisplayTime(timeRemaining);
        }
        else{
            timeRemaining = 0f;
            DisplayTime(timeRemaining);
        }
        yield return new WaitForSeconds(0.5f);
        textMeshProUGUI.color = Color.white;
        DisplayTime(timeRemaining);
    }
    public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        //Formatar a escala de tempo
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public bool IsTimeUp()
{
    return timeRemaining <= 0f; // Supondo que currentTime é o tempo restante
}

}
