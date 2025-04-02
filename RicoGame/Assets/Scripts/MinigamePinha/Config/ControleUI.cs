using UnityEngine;
using System.Collections;


public class ControleUI : MonoBehaviour
{
    [SerializeField] private StatusRico statusRico; 
    [SerializeField] private Temporizador temporizador; // Referência ao script 
    [SerializeField] private Rico rico;
    [SerializeField] private GameObject winPanel; 
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject diedPanel;
    [SerializeField] private float delayDead = 3.5f;
     

    private void Start()
    {
    
        if (statusRico == null) statusRico = FindObjectOfType<StatusRico>();
        if (temporizador == null) temporizador = FindObjectOfType<Temporizador>();
         if (rico == null) rico = FindObjectOfType<Rico>();

        rico.OnDeath += () => StartCoroutine(ShowDeadPanelWithDelay());
        if (winPanel != null) winPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (diedPanel != null) diedPanel.SetActive(false);
    }

    private void Update()
    {
           if (rico != null && rico.ricoDied) // Supondo que 'ricoDied' é a variável de morte
        { 
            ShowDeadPanel();
        } 
        // Verifica se o jogador tem 30+ pinhas e se o tempo acabou
        if (temporizador != null && temporizador.IsTimeUp())
        {
           
            
            if (statusRico != null && statusRico.pineCones >= 30)
            {
                ShowWinPanel(); // Vitória
            }
            else
            {
                ShowGameOverPanel(); // Derrota por não coletar pinhas suficientes
            }
            
        }
    
    }

    private void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f; // Pausa o jogo
        }
        else
        {
            Debug.LogWarning("WinPanel não atribuído no Inspector!");
        }
    }
    private void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
       // else Debug.LogWarning("GameOverPanel não atribuído!");
    }

    private IEnumerator ShowDeadPanelWithDelay()
    {
        yield return new WaitForSeconds(delayDead); // Espera o tempo definido
        ShowDeadPanel(); // Mostra o painel após o delay
    }


    private void ShowDeadPanel()
    {
         //Debug.Log("Tentando mostrar painel de morte");
        if(diedPanel !=null){
            diedPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        /* else
        {
            Debug.LogWarning("dead não atribuído no Inspector!");
        }
       */

    }
     

}