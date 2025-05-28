using System;
using UnityEngine;

namespace Game{
    public class Settings : MonoBehaviour
    {
        private SaveLoadSystem _saveLoadSystem;
        public float volumeMusic;
        public float volumeMaster;
        public bool effects;
        
        public static event Action OnLoadSettings;
        
        public void LoadSettings() {
            if(_saveLoadSystem.settingsData != null) {
                volumeMusic = _saveLoadSystem.settingsData.volumeMusic;
                volumeMaster = _saveLoadSystem.settingsData.volumeMaster;
                effects = _saveLoadSystem.settingsData.effects;
                OnLoadSettings?.Invoke();
            }
        }
        private void Awake() {
            if (FindObjectsOfType<Settings>().Length > 1) {
                Destroy(gameObject);
            }
            else {
                DontDestroyOnLoad(gameObject);
            }
            GameObject saveLoadManager =  GameObject.Find("SaveLoadManager");
            if (saveLoadManager != null){
                _saveLoadSystem = saveLoadManager.GetComponent<SaveLoadSystem>();
            }
        }

        private void Start() {
            LoadSettings();
        }

        public void SaveSettings(){
            _saveLoadSystem.settingsData.volumeMusic = volumeMusic;
            _saveLoadSystem.settingsData.volumeMaster = volumeMaster;
            _saveLoadSystem.settingsData.effects = effects;

            _saveLoadSystem.SaveSettingsData();
        }
    }

}
