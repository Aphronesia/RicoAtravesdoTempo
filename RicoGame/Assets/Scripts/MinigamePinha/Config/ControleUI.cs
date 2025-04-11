using UnityEngine;
using System.Collections;

public class ControleUI : MonoBehaviour

{
    public static ControleUI Instance { get; private set; }
    [Header("Referências")]
    [SerializeField] private StatusRico statusRico; 
    [SerializeField] private Temporizador temporizador; // Referência ao script 
    [SerializeField] private Rico rico;

    [Header("UI")]
    [SerializeField] private GameObject winPanel; 
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject diedPanel;

     [Header("Configurações")]
    [SerializeField] private float delayDead = 2.5f;
     
      private void Awake()
    {
        // Implementação do singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém entre cenas se necessário
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
    
        if (statusRico == null) statusRico = FindObjectOfType<StatusRico>();
        if (temporizador == null) temporizador = FindObjectOfType<Temporizador>();
        if (rico == null) rico = FindObjectOfType<Rico>();
        if (winPanel != null) winPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        
    }

    private void Update()
    {
       
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
        if (gameOverPanel != null){
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
      
    }

    public void ShowDiedPanelComDelay()
{
    StartCoroutine(ShowDiedPanelAfterDelay());
}

private IEnumerator ShowDiedPanelAfterDelay()
{
    yield return new WaitForSeconds(delayDead);

    if (diedPanel != null)
    {
        diedPanel.SetActive(true);
        Time.timeScale = 0f; // Pausa o jogo, se for o caso
    }
    else
    {
        Debug.LogWarning("DiedPanel não atribuído no Inspector!");
    }
}

}