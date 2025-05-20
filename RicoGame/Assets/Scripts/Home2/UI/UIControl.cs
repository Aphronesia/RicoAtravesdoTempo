using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Home2.UI{
    public class UIControl : MonoBehaviour
    {
        [SerializeField] private float transitionDuration;
        [Header("Settings Menu")]
        [SerializeField]
        private RectTransform panelSettingRT;

        
        [SerializeField]
        private Vector2 onPanelPos;
        [SerializeField]
        private Vector2 offPanelPos;
        [SerializeField]
        private float durationMoveUI;
        [SerializeField]
        private bool isOnSettings;
        
        [Header(("UI Objects"))]
        [SerializeField]
        private Slider sMusic;
        [SerializeField]
        private Slider sMaster;
        [SerializeField]
        private Toggle tEffects;
        [SerializeField]
        private TextMeshProUGUI continueText;

        public Color canContinue;
        public Color canNotContinue;
        
        private CanvasGroup _panelTransition;
        private bool _hasGameData;
        //Componentes
        private Game.Settings _settings;
        private Game.SaveLoadSystem _saveLoadSystem;
        private Game.ControlScenes _controlScene;
        private void OnEnable(){
            Game.Settings.OnLoadSettings += OnLoadSettingsUI;
            Game.SaveLoadSystem.OnLoadGame += OnLoadGameData;
        }
        private void OnDisable() {
            Game.Settings.OnLoadSettings -= OnLoadSettingsUI;
            Game.SaveLoadSystem.OnLoadGame -= OnLoadGameData;
        }
        public void OnLoadSettingsUI(){
            sMusic.value = _settings.volumeMusic;
            sMaster.value = _settings.volumeMaster;
            tEffects.isOn = _settings.effects;
        }

        private void OnLoadGameData(){
            _hasGameData = true;
            continueText.color = canContinue;
        }
        private void Start(){
            TakeComponents();
            offPanelPos = panelSettingRT.anchoredPosition;
            continueText.color = canNotContinue;

            _panelTransition.alpha = 0f;
            _panelTransition.interactable = false;
            _panelTransition.blocksRaycasts = false;

            if (_saveLoadSystem.hasSettingsData){
                _saveLoadSystem.LoadSettingsData();
            }

            if (_saveLoadSystem.hasGameData){
                _saveLoadSystem.LoadGameData();
            }
        }
        // Chamado pelo botão "Start" na UI.
        // Para quando for iniciar uma nova run do jogo.
        public void GameStart(){
            if (!_hasGameData){
                _panelTransition.alpha = 0f;
                StartCoroutine(UITransition());
            }
        }
        // Chamado pelo botão "Continue" na UI
        // Para quando for continuar a run do save. 
        public void GameContinue(){
            if (_hasGameData){
                _panelTransition.alpha = 0f;
                _saveLoadSystem.ContinueGame();
                StartCoroutine(UITransition());
            }
        }

        IEnumerator UITransition(){
            float startAlpha = _panelTransition.alpha;
            float time = 0f;
            while (time < transitionDuration){
                time += Time.deltaTime;
                _panelTransition.alpha = Mathf.Lerp(startAlpha, 1f, time / transitionDuration);
                yield return null;
            }
            _panelTransition.alpha = 1f;
            _panelTransition.interactable = true;
            _panelTransition.blocksRaycasts = true;
            
            _controlScene.ChangeScene(1);
        }
        // Chamado pelo botão "Settings" na UI.
        public void OpenSettingsMenu(){
            isOnSettings =! isOnSettings;
            StartCoroutine(!isOnSettings ? MoveMenuUI(offPanelPos) : MoveMenuUI(onPanelPos));

            IEnumerator MoveMenuUI(Vector3 endPos){
                Vector3 starPos = panelSettingRT.anchoredPosition;

                float elapsed  = 0f;
                while (elapsed < durationMoveUI){
                    elapsed += Time.deltaTime;
                    float t = Mathf.Clamp01 (elapsed /durationMoveUI);
                    panelSettingRT.anchoredPosition = Vector3.Slerp(starPos, endPos, t);
                    yield return null;
                }
                panelSettingRT.anchoredPosition = endPos;
            }
        }
        // Chamado pelo botão "Apply" na UI de Settings.
        public void SaveSettingsUI(){
            _settings.volumeMusic = sMusic.value;
            _settings.volumeMaster = sMaster.value;
            _settings.effects = tEffects.isOn;

            _settings.SaveSettings(); 
        }
        
        // Chamado pelo botão "Clear Data" na UI de Settings.
        public void ClearGameDataUI(){
            _saveLoadSystem.ClearData();
        }
        private void TakeComponents(){
            GameObject panelSettingsMenu = GameObject.Find("PanelSettings");
            if (panelSettingsMenu != null){
                panelSettingRT = panelSettingsMenu.GetComponent<RectTransform>();
            }
            GameObject sliderMusic = GameObject.Find("MusicSlider");
            if (sliderMusic != null){
                sMusic = sliderMusic.GetComponent<Slider>();
            }
            GameObject sliderMaster = GameObject.Find("MasterSlider");
            if (sliderMaster != null){
                sMaster = sliderMaster.GetComponent<Slider>();
            }
            GameObject toggleEffect = GameObject.Find("EffectsToggle");
            if (toggleEffect != null){
                tEffects = toggleEffect.GetComponent<Toggle>();
            }
            GameObject txtUiContinue = GameObject.Find("txtUIContinue");
            if (txtUiContinue != null){
                continueText = txtUiContinue.GetComponent<TextMeshProUGUI>();
            }
            GameObject uiTransition = GameObject.Find("UITransition");
            if (uiTransition != null){
                _panelTransition = uiTransition.GetComponent<CanvasGroup>();
            }
            Game.Settings gameSettings = FindObjectOfType<Game.Settings>();
            if (gameSettings != null){
                _settings = gameSettings.GetComponent<Game.Settings>();
            }
            Game.SaveLoadSystem saveLoadSystem = FindObjectOfType<Game.SaveLoadSystem>();
            if (saveLoadSystem != null){
                _saveLoadSystem = saveLoadSystem.GetComponent<Game.SaveLoadSystem>();
            }
            Game.ControlScenes controlScenes= FindObjectOfType<Game.ControlScenes>();
            if (controlScenes != null){
                _controlScene = controlScenes.GetComponent<Game.ControlScenes>();
            }
        }
    }
}
