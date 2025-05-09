using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Home{
    public class UIControl : MonoBehaviour
    {
        [Header("Settings Menu")]
        [SerializeField]
        private RectTransform panelSettingRT;
        private CanvasGroup settingsCanvasGroup;   
        private Game.Settings _settings;
        [SerializeField]
        private Vector2 OnPanelPos;
        [SerializeField]
        private Vector2 OffPanelPos;
        [SerializeField]
        private float durationMoveUI;
        [SerializeField]
        private bool openSettings;


        private Slider _sMusic;
        private Slider _sMaster;

        private Toggle _tEffects;
        private void OnEnable() {
            Game.Settings.OnLoadSettings += LoadSettingsUI;
        }
        private void OnDisable() {
            Game.Settings.OnLoadSettings -= LoadSettingsUI;
        }
        public void LoadSettingsUI(){
            _sMusic.value = _settings.volumeMusic;
            _sMaster.value = _settings.volumeMaster;
            _tEffects.isOn = _settings.effects;
        }
        private void Start() {
            Game.Settings GameSettings = FindObjectOfType<Game.Settings>();
            if (GameSettings != null){
                _settings = GameSettings.GetComponent<Game.Settings>();
            }
            GameObject sliderMusic = GameObject.Find("MusicSlider");
            if (sliderMusic != null){
                _sMusic = sliderMusic.GetComponent<Slider>();
            }
            GameObject sliderMaster = GameObject.Find("MasterSlider");
            if (sliderMaster != null){
                _sMaster = sliderMaster.GetComponent<Slider>();
            }
            GameObject toggleEffect = GameObject.Find("EffectsToggle");
            if (toggleEffect != null){
                _tEffects = toggleEffect.GetComponent<Toggle>();
            }
            GameObject panelSettingsMenu = GameObject.Find("PanelSettings");
            if (panelSettingsMenu != null){
                panelSettingRT = panelSettingsMenu.GetComponent<RectTransform>();
                settingsCanvasGroup = panelSettingsMenu.GetComponent<CanvasGroup>();
                OnPanelPos = panelSettingRT.anchoredPosition;
            }

        }
        public void SaveSettingsUI(){
            _settings.volumeMusic = _sMusic.value;
            _settings.volumeMaster = _sMaster.value;
            _settings.effects = _tEffects.isOn;

            _settings.SaveSettings(); 
        }
        public void OpenSettingsMenu(){
            openSettings =! openSettings;
            if (openSettings){
                StartCoroutine(MoveMenuUI(OnPanelPos));
            }
            else{
                StartCoroutine(MoveMenuUI(OffPanelPos));
            }
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
    }
}
