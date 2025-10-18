using System.Collections;
using Game;
using UnityEngine;

namespace MinigameTrem{
    public class ControleUITrem : MonoBehaviour
    {
        [SerializeField]
        private GameObject StartUI, MorreuUI, PauseUI, winUI;
        public CapivaraTrem capivaratrem;
        public ControlScenes controlScenes;
        public Monstros monstros;
        public Estrelas estrelas;
        public Scoreboard.ScoreManager scoreManager;
        private SaveLoadSystem _saveLoadSystem;
        private ControlSounds _controlSounds;
    
        bool paused;
        
        private void OnEnable(){
            Scoreboard.ScoreManager.OnGanhou += Ganhou;
        }

        private void OnDisable(){
            Scoreboard.ScoreManager.OnGanhou -= Ganhou;
        }
        // Start is called before the first frame update
        void Start(){
            TakeComponents();
            
            MorreuUI.SetActive(false);
            winUI.SetActive(false);
            StartUI.SetActive(true);
            
            //controla rotacao de tela (muda prapaisagem)
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            
            // SOMMMMM
            _controlSounds.PlayMusic("Trem");  
        }

        //click do play inicial do jogo
        public void StartGame()
        {
            //Debug.Log("ControleUI iniciou");
            StartUI.SetActive(false);
            capivaratrem.StartGame();
            monstros.StartGame();
            scoreManager.StartRun();
            estrelas.canMove = true;
        }
        IEnumerator Faleceu()
        {
            yield return new WaitForSeconds(1.5f);
            MorreuUI.SetActive(true);
        }
        public void Morreu()
        {
            scoreManager.StopRun();
            StartCoroutine(Faleceu());
        }

        public void Ganhou(){
            StartCoroutine(Win());
            IEnumerator Win()
            {
                yield return new WaitForSeconds(2f);
                winUI.SetActive(true);
            }
            if (_saveLoadSystem.runtimeGameData.levelCompleted <= 2){
                _saveLoadSystem.runtimeGameData.levelCompleted = 2;
            }
            _saveLoadSystem.runtimeGameData.menuMapRico = 1;
            
        }
        public void Pause()
        {
            if (!paused)
            {
                paused = true;
                Time.timeScale = 0f;
                PauseUI.SetActive(true);
                scoreManager.StopRun();
            }
            else
            {
                paused = false;
                Time.timeScale = 1f;
                PauseUI.SetActive(false);
                scoreManager.StartRun();
            }
        }
        public void Recomeca()
        {
            controlScenes.RestartGame();
        }
        public void QuitGame()
        {
            controlScenes.QuitGame();
        }
        public void ReturnHome()
        {
            //StartCoroutine(Transition(0));
            controlScenes.ReturnHome();
        }
        public void ReturnMenuMap(){
            //StartCoroutine(Transition(2));
            controlScenes.ReturnMenuMap();
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
            controlScenes.ChangeScene(value); // menu map
        }

        private void TakeComponents(){
            //procura o controlador de cenas
            ControlScenes ScenesController = FindObjectOfType<ControlScenes>();
            if (ScenesController != null){
                controlScenes = ScenesController.GetComponent<ControlScenes>();
            }
            else{
                Debug.LogError("Objeto indestrutível não encontrado!"); 
            }
            GameObject saveLoadManager =  GameObject.Find("SaveLoadManager");
            if (saveLoadManager != null){
                _saveLoadSystem = saveLoadManager.GetComponent<SaveLoadSystem>();
            }
            var soundManager = FindAnyObjectByType<ControlSounds>();
            if (soundManager is not null)
            {
                _controlSounds = soundManager.GetComponent<ControlSounds>();
            }
        }
    }
}
