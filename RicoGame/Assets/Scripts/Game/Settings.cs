using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game{
    public class Settings : MonoBehaviour
    {
        private SaveLoadSystem _saveLoadSystem;
        public float volumeMusic;
        public float volumeMaster;
        public bool effects;
        
        private void OnEnable(){
            SaveLoadSystem.OnLoadSettings += LoadSettings;
        }
        private void OnDisable() {
            SaveLoadSystem.OnLoadSettings -= LoadSettings;
        }

        private void LoadSettings(){
            if(_saveLoadSystem.settingsData != null){
                volumeMusic = _saveLoadSystem.settingsData.volumeMusic;
                volumeMaster = _saveLoadSystem.settingsData.volumeMaster;
                effects = _saveLoadSystem.settingsData.effects;
            }
        }
        private void Awake() {
            if (FindObjectsOfType<Settings>().Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
            GameObject saveLoadManager =  GameObject.Find("SaveLoadManager");
            if (saveLoadManager != null){
                _saveLoadSystem = saveLoadManager.GetComponent<SaveLoadSystem>();
            }
        }
    }

}
