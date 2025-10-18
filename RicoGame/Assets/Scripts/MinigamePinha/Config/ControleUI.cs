using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Game;
using UnityEngine.SceneManagement;

public class ControleUI : MonoBehaviour

{
    public static ControleUI Instance { get; private set; }
    [Header("Referências")]
    [SerializeField] private StatusRico statusRico; 
    [SerializeField] private Temporizador temporizador; // Referência ao script 
    [SerializeField] private Rico rico;
    [SerializeField] private GameObject winPanel, gameOverPanel,diedPanel,pausePanel,startPanel; 
      public ControlScenes controlScenes;


     [Header("Configurações")]
    [SerializeField] private float delayDead = 2.5f;

     private bool startGame = false;

     private SaveLoadSystem _saveLoadSystem;
    
     private void OnEnable() {
         UIGeral.ConfigUI.OnPause += PauseGame;
     }
     private void OnDisable() {
         UIGeral.ConfigUI.OnPause -= PauseGame;
     }
    private void Start()
    {
        // Rotaciona a tela 
        // Ferle que colocou isso ;)
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        
        
        Time.timeScale = 0f; // Começa pausado
    
        if (statusRico == null) statusRico = FindObjectOfType<StatusRico>();
        if (temporizador == null) temporizador = FindObjectOfType<Temporizador>();
        if (rico == null) rico = FindObjectOfType<Rico>();
        if (winPanel != null) winPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(false);
        if (startPanel != null) startPanel.SetActive(true);
        
        
        GameObject saveLoadManager =  GameObject.Find("SaveLoadManager");
        if (saveLoadManager != null){
            _saveLoadSystem = saveLoadManager.GetComponent<SaveLoadSystem>();
        }
    }
     private void Awake()
    {
        // Implementação do singleton
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Mantém entre cenas se necessário
        }
        else
        {
            Destroy(gameObject);
        }
    }

 

    private void Update()
    {
        if(!startGame) return;
        // Verifica se o jogador tem 30+ pinhas e se o tempo acabou
        if (temporizador != null && temporizador.IsTimeUp()){
           
            if (statusRico != null && statusRico.pineCones >= 30){
                ShowWinPanel(); // Vitória
            }
            else{
                ShowGameOverPanel(); // Derrota por não coletar pinhas suficientes
            }
        }
    }

    public void ShowWinPanel() {
        if (_saveLoadSystem.runtimeGameData.levelCompleted <= 1){
            _saveLoadSystem.runtimeGameData.levelCompleted = 1;
        }
        _saveLoadSystem.runtimeGameData.menuMapRico = 0;
        if (winPanel != null){
            winPanel.SetActive(true);
            Time.timeScale = 0f; // Pausa o jogo
        }
        else{
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

        if (diedPanel != null){
            diedPanel.SetActive(true);
            Time.timeScale = 0f; // Pausa o jogo, se for o caso
        }
        else{
            Debug.LogWarning("DiedPanel não atribuído no Inspector!");
        }
    }
    
    // Metodo antigo do dom pra chamar o pause
       public void OnButtonClick()
    {
        if (pausePanel != null){
            pausePanel.SetActive(true); // Mostra o painel de pausa
            Time.timeScale = 0f;        // Pausa o jogo (opcional)
            Debug.Log("Painel de pause ativado.");
        }
        else{
            Debug.LogWarning("pausePanel não foi atribuído no Inspetor!");
        }
    }
    
    public void StartGame()
    {
        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }

        Time.timeScale = 1f;
        startGame = true;

         if (temporizador != null)
          temporizador.Comeca();
        
    }

    public void ResumeGame()
    {
        if (pausePanel != null){
            pausePanel.SetActive(false);
            Time.timeScale = 1f; // Continua o jogo
            Debug.Log("Jogo retomado.");
        }
    }

    private void PauseGame(bool pause) {
        
    }
    public void ReturnToStartPanel()
    {
         Time.timeScale = 1f; 
         controlScenes.RestartGame();
    }

    public void ReturnHome(){
        Time.timeScale = 1f; 
        controlScenes.ReturnHome();
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; 
        controlScenes.RestartGame();
    }

    public void ReturnMenuMap()
    {
        Time.timeScale = 1f; 
        
        controlScenes.ReturnMenuMap();
    }
    public void QuitGame(){
        controlScenes.QuitGame();
    }
    [SerializeField] private float durationFade;
    [SerializeField] private CanvasGroup UITransition; 
    private IEnumerator Transition(int value)
    {
        Time.timeScale = 1f;
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
        controlScenes.ChangeScene(value); // menu map
    }
}



