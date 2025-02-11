using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int points;
    public float tempoAcumulado = 0f;
    public bool poinsIsRunning;
    public TMP_Text timeText;
    public TextMeshProUGUI textMeshProUGUI;
    public ScoreboardTremControl tremControl;
    // Start is called before the first frame update
    void Start()
    {
        points = 0;
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
            if (tempoAcumulado >= 0.5f)  // Verifica se 1 segundo se passou
            {
                points++;
                tempoAcumulado = 0f;     // Reseta o contador
            }
        }
    }
}
