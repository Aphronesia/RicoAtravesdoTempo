using System;
using UnityEngine;

namespace Game{
    public class Settings : MonoBehaviour
    {
        private SaveLoadSystem _saveLoadSystem;
        public float volumeMusic;
        public float volumeMaster;
        public bool effects;
        
        private ControlSounds _controlSounds;
        public static event Action OnLoadSettings;
        public void LoadSettings() {
            if(_saveLoadSystem.settingsData != null) {
                volumeMusic = _saveLoadSystem.settingsData.volumeMusic;
                volumeMaster = _saveLoadSystem.settingsData.volumeMaster;
                effects = _saveLoadSystem.settingsData.effects;
                OnLoadSettings?.Invoke();
                
                _controlSounds.ChangeVolumes();
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

        private void Start()
        {
            TakeComponents();
            LoadSettings();
            
            
        }

        public void SaveSettings(){
            _saveLoadSystem.settingsData.volumeMusic = volumeMusic;
            _saveLoadSystem.settingsData.volumeMaster = volumeMaster;
            _saveLoadSystem.settingsData.effects = effects;

            _saveLoadSystem.SaveSettingsData();
            
            _controlSounds.ChangeVolumes();
        }

        private void TakeComponents()
        {
            GameObject soundManager = FindAnyObjectByType<ControlSounds>().gameObject;
            if (soundManager != null)
            {
                _controlSounds = soundManager.GetComponent<ControlSounds>();
            }
        }
    }
}
