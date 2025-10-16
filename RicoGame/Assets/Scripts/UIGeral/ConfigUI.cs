using System;
using System.Collections;
using Game;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

namespace UIGeral {
    public class ConfigUI : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField]
        public bool pause;
        
        [Header("Menu UI")]
        [SerializeField]
        private GameObject uiMenuPause;
        private RectTransform _menuRt;
        [SerializeField]
        private Vector3 menuOutPos;
        [SerializeField]
        private float durationMoveUI;

        [Header("Painel de salvamento")]
        [SerializeField]
        private RectTransform panelSaveRect;
        [SerializeField]
        private Vector2 onPanelPos;
        [SerializeField]
        private Vector2 offPanelPos;

        [SerializeField] private int index;
        
        [Header("Painel de configurações")]
        [SerializeField]
        private RectTransform panelSettingRT;
        [SerializeField]
        private Slider sMusic;
        [SerializeField]
        private Slider sMaster;
        [SerializeField]
        private Toggle tEffects;
        [SerializeField]
        private Vector2 onSettingsPos;
        [SerializeField]
        private Vector2 offSettingsPos;
        [SerializeField]
        private bool isOnSettings;

        private bool _saved;
        private Settings _settings;
        private CanvasGroup _panelTransition;
    
        private ControlScenes _controlScenes;
        private SaveLoadSystem _saveLoadSystem;
        private ControlSounds _controlSounds;

        public static event Action<bool> OnPause;
    
        private void OnEnable() {
        
        }
        private void OnDisable() {
        
        }
        public void OnLoadSettingsUI(){
            sMusic.value = _settings.volumeMusic;
            sMaster.value = _settings.volumeMaster;
            tEffects.isOn = _settings.effects;
        }
        private void Start() {
            TakeComponents();
            panelSaveRect.anchoredPosition = offPanelPos;
            panelSettingRT.anchoredPosition = offSettingsPos;
            OnLoadSettingsUI();
            //Time.timeScale = 1f;
        
        }
        public void Home(){
            _saveLoadSystem.SaveGameData();
            _controlScenes.ReturnHome();
        }
        public void Quit(){
            _controlScenes.QuitGame();
        }

        public void Settings() {
            
        }
        public void ShowSavePanel(int value) {
            _controlSounds.PlaySfx("button");
            panelSaveRect.anchoredPosition = onPanelPos ;
            index = value;
            isOnSettings = true;
            ShowHideSettingsPanel();
        }

        public void HideSavePanel() {
            
            panelSaveRect.anchoredPosition = offPanelPos;
        }

        public void ShowHideSettingsPanel() {
            _controlSounds.PlaySfx("button");
            if (!isOnSettings){
                HideSavePanel();
                panelSettingRT.anchoredPosition = onSettingsPos;
                isOnSettings = true;
            }
            else {
                panelSettingRT.anchoredPosition = offSettingsPos;
                isOnSettings = false;
            }
        }
        public void SaveSettingsUI(){
            _settings.volumeMusic = sMusic.value;
            _settings.volumeMaster = sMaster.value;
            _settings.effects = tEffects.isOn;

            _settings.SaveSettings(); 
        }
        public void ButtonSave(bool save) {
            _controlSounds.PlaySfx("button");
            if (save) {
                _saveLoadSystem.SaveGameData();
            }
            switch (index) {
                case 0:
                    _controlScenes.ReturnHome();
                    break;
                case 1:
                    _controlScenes.QuitGame();
                    break;
            }
        }
        public void Pause(){
            _controlSounds.PlaySfx("button");
            pause =! pause;
            OnPause?.Invoke(pause);

            if(pause){
                //Centralizar o menu de Pause
                _menuRt.anchorMin = new Vector2(0.5f, 0.5f);
                _menuRt.anchorMax = new Vector2(0.5f, 0.5f);
                _menuRt.pivot = new Vector2(0.5f, 0.5f);
                //_menuRt.anchoredPosition = Vector2.zero;
                StartCoroutine(MoveMenuUI(Vector3.zero));
            }
            else{
                _controlScenes.Pause(pause);
                //_menuRt.anchoredPosition = menuOutPos;
                HideSavePanel();
                if (isOnSettings) {
                    ShowHideSettingsPanel();
                }
                StartCoroutine(MoveMenuUI(menuOutPos));
            }

            IEnumerator MoveMenuUI(Vector3 endPos){
                Vector3 starPos = _menuRt.anchoredPosition;

                float elapsed  = 0f;
                while (elapsed < durationMoveUI){
                    elapsed += Time.deltaTime;
                    float t = Mathf.Clamp01 (elapsed /durationMoveUI);
                    _menuRt.anchoredPosition = Vector3.Slerp(starPos, endPos, t);
                    yield return null;
                }
                _menuRt.anchoredPosition = endPos;
                _controlScenes.Pause(pause);
                //o jogo vai parar só depois que o movimento for concluido
                //RESOLVER ISSO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
        }
        private void TakeComponents() {
            uiMenuPause = GameObject.Find("UIMenuPause");
            if (uiMenuPause != null) {
                _menuRt = uiMenuPause.GetComponent<RectTransform>();
            }
            GameObject uiSave = GameObject.Find("UISave");
            if (uiSave != null) {
                panelSaveRect = uiSave.GetComponent<RectTransform>();
            }
            ControlScenes scenesController = FindObjectOfType<ControlScenes>();
            if (scenesController != null){
                _controlScenes = scenesController.GetComponent<ControlScenes>();
            }
            SaveLoadSystem saveLoadSystem = FindObjectOfType<SaveLoadSystem>();
            if (saveLoadSystem != null) {
                _saveLoadSystem = saveLoadSystem.GetComponent<SaveLoadSystem>();
            }
            Settings gameSettings = FindObjectOfType<Settings>();
            if (gameSettings != null){
                _settings = gameSettings.GetComponent<Settings>();
            }
            var soundManager = FindAnyObjectByType<ControlSounds>();
            if (soundManager is not null)
            {
                _controlSounds = soundManager.GetComponent<ControlSounds>();
            }
            
            
            GameObject panelSettingsMenu = GameObject.Find("UISettings");
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
            GameObject uiTransition = GameObject.Find("UITransition");
            if (uiTransition != null){
                _panelTransition = uiTransition.GetComponent<CanvasGroup>();
            }
            
            
        }
    }
}
